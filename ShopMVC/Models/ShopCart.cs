using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Models
{
    [Table("ShopCart")]
    public class ShopCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool isDeleted { get; set; }=false;

        public ICollection<CartDetail> CartDetails { get; set; }

    }
}
