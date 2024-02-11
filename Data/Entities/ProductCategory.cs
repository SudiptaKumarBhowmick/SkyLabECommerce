using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductCategory : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required!!")]
        [MaxLength(250, ErrorMessage = "Category name is too long")]
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<ProductSubCategory> ProductSubCategories { get; } = new List<ProductSubCategory>();
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
