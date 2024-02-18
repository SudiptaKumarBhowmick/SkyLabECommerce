using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OrderStatusDto
    {
        public int Id { get; set; }
        [Required]
        public int StatusCode { get; set; }
        [Required(ErrorMessage = "Status field is required!!")]
        [MaxLength(250, ErrorMessage = "Status is too long")]
        public required string Status { get; set; }
    }
}
