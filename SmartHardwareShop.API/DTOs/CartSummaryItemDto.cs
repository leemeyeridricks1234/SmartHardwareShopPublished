﻿namespace SmartHardwareShop.API.DTOs
{
    public class CartSummaryItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal SubTotal
        {
            get
            {
                return Price * Quantity;
            }
        }
        
    }
}
