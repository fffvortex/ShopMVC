using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Models.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string? ItemTitle { get; set; }

        [Required]
        public double Price { get; set; }

        public string? Image { get; set; }

        [MaxLength(371)]
        public string? Description { get; set; }

        [Required]
        public int TypeId { get; set; }

        [MaxLength(170)]
        public string Stats { get; set; }

        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem>? TypeList { get; set; }
    }
}
