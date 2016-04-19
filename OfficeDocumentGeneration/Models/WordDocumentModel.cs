using System.ComponentModel.DataAnnotations;

namespace OfficeDocumentGeneration.Models
{
    public class WordDocumentModel
    {
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }
        [Display(Name = "Copy To")]
        public string CopyTo { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Summary")]
        public string Summary { get; set; }
        [Display(Name = "Conclusion")]
        public string Conclusion { get; set; }
        [Display(Name = "Recommendation")]
        public string Recommendation { get; set; }
        [Display(Name = "RegistrationNumber")]
        public string RegistrationNumber { get; set; }
    }
}