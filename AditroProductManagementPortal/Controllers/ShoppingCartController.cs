using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AditroProductManagementPortal.Models;

namespace AditroProductManagementPortal.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public void AddToCart(ProductModel product)
        {

        }
    }
}