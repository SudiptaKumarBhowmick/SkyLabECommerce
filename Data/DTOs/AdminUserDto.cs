using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class AdminUserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required!!")]
        [MinLength(3, ErrorMessage = "Username is too small")]
        [MaxLength(50, ErrorMessage = "Username is too long")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required!!")]
        [MinLength(8, ErrorMessage = "Password must be minimum 8 characters long")]
        [MaxLength(20, ErrorMessage = "Password must be maximum 20 characters long")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).*$", ErrorMessage = "Must contain one lowercase alphabet, uppercase alphabet, numeric digit and special character")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Email is required!!")]
        [MaxLength(150, ErrorMessage = "Email is too long")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,10}$", ErrorMessage = "Invalid email address")]
        public required string UserEmail { get; set; }
        public int UserTypeId { get; set; }
        public string TypeName { get; set; } = "";
    }
}
