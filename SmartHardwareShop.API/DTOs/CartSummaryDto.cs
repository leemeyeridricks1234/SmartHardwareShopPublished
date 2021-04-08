using System.Collections.Generic;
using System.Linq;

namespace SmartHardwareShop.API.DTOs
{
    public class CartSummaryDto
    {
        public decimal Total
        {
            get
            {
                if (Items == null) return 0;
                return Items.Select(x => x.SubTotal).Sum();
            }
        }

        public List<CartSummaryItemDto> Items { get; set; }

        public CartSummaryDto()
        {
            Items = new List<CartSummaryItemDto>();
        }
    }
}
