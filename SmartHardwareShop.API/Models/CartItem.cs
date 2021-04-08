using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHardwareShop.API.Models
{
    public class CartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }

        //public virtual ICollection<Login> Logins { get; set; }
    }
}
