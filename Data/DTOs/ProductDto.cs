using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required!!")]
        [MaxLength(250, ErrorMessage = "Product Name is too long")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Product description is required!!")]
        public required string Description { get; set; }
        public int ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
    }
}
