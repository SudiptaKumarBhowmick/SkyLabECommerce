
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Image file is required!!")]
        public required IFormFile File { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public string PublicId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product name is required!!")]
        public required int ProductId { get; set; }
    }
}
