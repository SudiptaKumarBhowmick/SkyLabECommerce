using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProductSubCategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Sub category name is required!!")]
        [MaxLength(250, ErrorMessage = "Sub category name is too long")]
        public required string SubCategoryName { get; set; }
        [Required(ErrorMessage = "Category is required!!")]
        public int ProductCategoryId { get; set; }
    }
}
