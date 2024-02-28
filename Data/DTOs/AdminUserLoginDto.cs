using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class AdminUserLoginDto
    {
        public int Id { get; set; }
        public required string UserName { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string UserEmail { get; set; } = string.Empty;
        public int UserTypeId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
