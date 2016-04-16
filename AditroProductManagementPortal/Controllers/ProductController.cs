using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AditroProductManagementPortal.Models;
using AditroProductMangement.Core.Products;
namespace AditroProductManagementPortal.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductFacade<ProductModel> _productFacade;
        //private IList<ProductModel> ImportedProductsFromSession;


        public ProductController()
        {
        }

        public ProductController(IProductFacade<ProductModel> productFacade)
        {
            _productFacade = productFacade;
        }

        public ActionResult Browse()
        {
            return View();
        }

        public ActionResult ProductAdministration()
        {
            var importedProducts = Session["ImportedProducts"] = new ProductFacade<ProductModel>().GetImportedProducts();
            return View(importedProducts);
        }

        public ActionResult Delete(int id)
        {
            var productsFromSession = (IList<ProductModel>)Session["ImportedProducts"];
            Session["ImportedProducts"] = productsFromSession.Select(prod => prod.Id != id);
            return ProductAdministration();
        }

        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

        public ActionResult Edit(int id)
        {
            //throw new NotImplementedException();
            var productsFromSession = (IList<ProductModel>)Session["ImportedProducts"];
            return View(productsFromSession.FirstOrDefault(product => product.Id == id));
        }

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }
    }
}