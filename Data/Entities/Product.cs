using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required!!")]
        [MaxLength(250, ErrorMessage = "Product Name is too long")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Product description is required!!")]
        public required string Description { get; set; }
        public ICollection<ProductImage> Images { get; } = new List<ProductImage>();

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } = null!;

        public int? ProductSubCategoryId { get; set; }
        public ProductSubCategory? ProductSubCategory { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
