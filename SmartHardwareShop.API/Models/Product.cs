using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHardwareShop.API.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public decimal RRP { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public DateTime DateAdded { get; set; }
        public int UserId { get; set; }

        //public virtual ICollection<Login> Logins { get; set; }
    }
}
