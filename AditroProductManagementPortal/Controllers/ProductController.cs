using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AditroProductManagementPortal.Models;
using AditroProductMangement.Core.Products;
using AutoMapper;

namespace AditroProductManagementPortal.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductFacade<ProductModel> _productFacade;
        //private IList<ProductModel> ImportedProductsFromSession;
        private const string ImportedProducts = "ImportedProducts";

        public ProductController()
        {
        }

        public ProductController(IProductFacade<ProductModel> productFacade)
        {
            _productFacade = productFacade;
        }

        public ActionResult ProductAdministration()
        {
            if (Session[ImportedProducts] == null)
            {
                var importedProducts = Session[ImportedProducts] = new ProductFacade<ProductModel>().GetImportedProducts();
                return View(importedProducts);
            }
            return View(Session[ImportedProducts]);
        }

        public ActionResult Delete(int id)
        {
            var productsFromSession = (List<ProductModel>)Session[ImportedProducts];
            productsFromSession.RemoveAll(p => p.Id == id);
            Session["ImportedProducts"] = productsFromSession;
            return RedirectToAction("ProductAdministration");
        }

        public ActionResult Details(int id)
        {
            var productsFromSession = (IList<ProductModel>)Session[ImportedProducts] ??
                                      (IList<ProductModel>)(Session[ImportedProducts] = new ProductFacade<ProductModel>().GetImportedProducts());
            return View(productsFromSession.FirstOrDefault(prod => prod.Id == id));
        }

        public ActionResult Edit(int id)
        {
            //throw new NotImplementedException();
            var productsFromSession = (IList<ProductModel>)Session[ImportedProducts];
            return View(productsFromSession.FirstOrDefault(product => product.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel product)
        {
            if (!ModelState.IsValid) return RedirectToAction("ProductAdministration");
            var productsFromSession = ((List<ProductModel>)Session[ImportedProducts]);
            var updatedProduct = Mapper.Map(product, productsFromSession.SingleOrDefault(p => p.Id == product.Id));
            productsFromSession.RemoveAll(p => p.Id == product.Id);
            productsFromSession.Add(updatedProduct);
            Session[ImportedProducts] = productsFromSession;
            return RedirectToAction("ProductAdministration");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    //var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                    var products = new ProductFacade<ProductModel>().UploadProuctCatalogue(file.FileName);
                    Session[ImportedProducts] = products;
                }
            }
            return RedirectToAction("ProductAdministration");
        }

        public ActionResult Save(int id)
        {
            return RedirectToAction("ProductAdministration");
        }
    }
}