using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ShopMVC.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }

        [Required]
        public int ShopCartId {  get; set; }
        [Required]
        public int ItemId {  get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }

        public Item Item { get; set; }

        public ShopCart ShopCart { get; set; }
    }
}
