﻿namespace AditroProductManagementPortal.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}