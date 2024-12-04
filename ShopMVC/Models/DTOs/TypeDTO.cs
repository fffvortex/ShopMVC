using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Models.DTOs
{
    public class TypeDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string TypeTitle { get; set; }
    }
}
