using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHardwareShop.API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        //public virtual ICollection<Login> Logins { get; set; }
    }
}
