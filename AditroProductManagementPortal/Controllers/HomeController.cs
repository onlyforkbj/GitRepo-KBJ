using System.Web.Mvc;
using AditroProductManagementPortal.Models;
using AditroProductMangement.Core.Products;

namespace AditroProductManagementPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            object importedProducts;
            if (Session["ImportedProducts"] != null)
            {
                importedProducts = Session["ImportedProducts"];
            }
            else
            {
                importedProducts = Session["ImportedProducts"] = new ProductFacade<ProductModel>().GetImportedProducts();
            }
            return View(importedProducts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}