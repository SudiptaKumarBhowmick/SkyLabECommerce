using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class ProductCategoryDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required!!")]
        [MaxLength(250, ErrorMessage = "Category name is too long")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
