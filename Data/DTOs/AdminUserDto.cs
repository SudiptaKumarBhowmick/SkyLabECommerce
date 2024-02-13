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
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string UserEmail { get; set; }
        public int UserTypeId { get; set; }
    }
}
