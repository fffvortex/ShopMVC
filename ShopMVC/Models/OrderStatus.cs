using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        public int Id { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required,MaxLength(20, ErrorMessage ="Max length 20 symbols")]
        public string? StatusName { get; set; }
    }
}
