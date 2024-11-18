using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Models
{
    [Table("ItemType")]
    public class TypeItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20,ErrorMessage = "no more than 20 characters")]
        public string TypeTitle { get; set; }

        public List<Item> Items { get; set; }
    }
}
