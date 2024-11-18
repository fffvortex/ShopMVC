using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Models
{
    [Table("Item")]
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "no more than 20 characters")]
        public string ItemTitle { get; set; }

        [MaxLength(100, ErrorMessage = "no more than 100 characters")]
        public string? Description { get; set; }

        public int TypeId { get; set; }

        public TypeItem ItemType;
    }
}
