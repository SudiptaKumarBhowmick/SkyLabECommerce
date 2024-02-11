using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductSubCategory : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Sub category name is required!!")]
        [MaxLength(250, ErrorMessage = "Sub category name is too long")]
        public required string SubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
