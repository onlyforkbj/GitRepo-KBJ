using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AditroProductManagementPortal.Models;
using AditroProductManagementPortal.ViewModels;

namespace AditroProductManagementPortal.Controllers
{
    public class ShoppingCartController : Controller
    {
        string ShoppingCartId { get; set; }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            var cartItems = cart.GetCartItems(HttpContext);
            var cartTotal = cart.GetTotal(HttpContext);
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                CartTotal = cartTotal
            };

            // Return the view
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            // Retrieve the Product from Session
            var addedAlbum = ((IList<ProductModel>)Session["ImportedProducts"]).SingleOrDefault(p => p.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            Session["ShoppingCart"] = cart.AddToCart(addedAlbum, HttpContext);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
    }
}