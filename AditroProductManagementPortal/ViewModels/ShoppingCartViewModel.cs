using System.Collections.Generic;
using AditroProductManagementPortal.Models;

namespace AditroProductManagementPortal.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            CartItems = new List<Cart>();
        }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}