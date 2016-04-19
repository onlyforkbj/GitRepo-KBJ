using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Reflection;
using WebGrease.Css.Extensions;

namespace OfficeDocumentGeneration.Models
{
    public static class WordDocumentGenerator
    {
        public static WordprocessingDocument InsertText(this WordprocessingDocument doc, string contentControlTag, string text)
        {
            SdtElement element = doc.MainDocumentPart.Document.Body.Descendants<SdtElement>()
              .FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == contentControlTag);

            if (element == null)
                throw new ArgumentException($"ContentControlTag \"{contentControlTag}\" doesn't exist.");

            element.Descendants<Text>().First().Text = text;
            element.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());

            return doc;
        }

        internal static WordprocessingDocument RemoveSdtBlocks(this WordprocessingDocument doc, IList<string> contentBlocks)
        {
            List<SdtElement> sdtBlocks = doc.MainDocumentPart.Document.Descendants<SdtElement>().ToList();

            if (contentBlocks == null)
                return doc;

            foreach (var s in contentBlocks)
            {
                SdtElement currentElement = sdtBlocks.FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == s);
                if (currentElement == null)
                    continue;
                IEnumerable<OpenXmlElement> elements = null;

                if (currentElement is SdtBlock)
                    elements = (currentElement as SdtBlock).SdtContentBlock.Elements();
                else if (currentElement is SdtCell)
                    elements = (currentElement as SdtCell).SdtContentCell.Elements();
                else if (currentElement is SdtRun)
                    elements = (currentElement as SdtRun).SdtContentRun.Elements();

                foreach (var el in elements)
                    currentElement.InsertBeforeSelf(el.CloneNode(true));
                currentElement.Remove();
            }
            return doc;
        }
        public static void GenerateOfficeDocument(WordDocumentModel documentModel)
        {
            string destinationFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DocumentReport.docx");
            string sourceFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "YaraDocReport.dotx");
            try
            {
                // Create a copy of the template file and open the copy
                File.Copy(sourceFile, destinationFile, true);
                using (WordprocessingDocument document = WordprocessingDocument.Open(destinationFile, true))
                {
                    //     SdtElement element = document.MainDocumentPart.Document.Body.Descendants<SdtElement>()
                    //.FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == documentModel.FaxNumber);

                    //if (element == null)
                    //    throw new ArgumentException($"ContentControlTag \"{contentControlTag}\" doesn't exist.");

                    IList<string> contentBlock = new List<string>();

                    foreach (var property in documentModel.GetType().GetProperties())
                    {
                        contentBlock.Add(property.Name);
                        var tagInfo = document.MainDocumentPart.Document.Body.Descendants<SdtElement>()
                            .FirstOrDefault(sdt => sdt.SdtProperties.GetFirstChild<Tag>()?.Val == property.Name);
                        if (tagInfo != null) tagInfo.Descendants<Text>().First().Text = Convert.ToString(property.GetValue(documentModel, null));
                    }

                    RemoveSdtBlocks(document, contentBlock);

                    //if (element != null)
                    //{
                    //    element.Descendants<Text>().First().Text = "My Fax Number";
                    //    //element.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                    //}

                    //document.MainDocumentPart.Document.Body.Descendants<SdtElement>().ForEach(el =>
                    //{
                    //    if (el.SdtProperties.GetFirstChild<Tag>()?.Val != documentModel.FaxNumber) return;
                    //    el.Descendants<Text>().First().Text = "My Fax Number";
                    //    el.Descendants<Text>().Skip(1).ToList().ForEach(t => t.Remove());
                    //});

                    // Change the document type to Document
                    document.ChangeDocumentType(WordprocessingDocumentType.Document);

                    // Get the MainPart of the document
                    MainDocumentPart mainPart = document.MainDocumentPart;

                    // Get the Document Settings Part
                    DocumentSettingsPart documentSettingPart1 = mainPart.DocumentSettingsPart;

                    // Create a new attachedTemplate and specify a relationship ID
                    AttachedTemplate attachedTemplate1 = new AttachedTemplate() { Id = "relationId1" };

                    // Append the attached template to the DocumentSettingsPart
                    documentSettingPart1.Settings.Append(attachedTemplate1);

                    // Add an ExternalRelationShip of type AttachedTemplate.
                    // Specify the path of template and the relationship ID
                    documentSettingPart1.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", new Uri(sourceFile, UriKind.Absolute), "relationId1");

                    // Save the document
                    mainPart.Document.Save();

                    Console.WriteLine("Document generated at " + destinationFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine(@"\nPress Enter to continue…");
                Console.ReadLine();
            }
        }
    }
}
