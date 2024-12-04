using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Models
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string ItemTitle { get; set; }

        [Required]
        public double Price { get; set; }

        public string? Image { get; set; }

        [MaxLength(371)]
        public string? Description { get; set; }

        [Required]
        public int TypeId { get; set; }

        [MaxLength(170)]
        public string Stats { get; set; }

        public TypeItem ItemType { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string TypeName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }
    }
}
