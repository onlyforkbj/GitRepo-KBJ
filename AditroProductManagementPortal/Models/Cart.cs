using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AditroProductManagementPortal.Models
{
    public class Cart
    {
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }

        public ProductModel Product { get; set; }
    }
}