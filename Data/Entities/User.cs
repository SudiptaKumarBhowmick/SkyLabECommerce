using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required!!")]
        [MinLength(3, ErrorMessage = "Username is too small")]
        [MaxLength(50, ErrorMessage = "Username is too long")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!!")]
        [MinLength(8, ErrorMessage = "Password must be minimum 8 characters long")]
        [MaxLength(20, ErrorMessage = "Password must be maximum 20 characters long")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])$", ErrorMessage = "Must contain one lowercase alphabet, uppercase alphabet, numeric digit and special character")]
        public required string Password { get; set; }

        public string? UserEmail { get; set; }
        public string? Mobile { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
