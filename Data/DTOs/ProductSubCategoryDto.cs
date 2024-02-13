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
        public required string SubCategoryName { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
