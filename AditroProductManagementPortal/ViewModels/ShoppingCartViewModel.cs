using System.Collections.Generic;
using AditroProductManagementPortal.Models;

namespace AditroProductManagementPortal.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}