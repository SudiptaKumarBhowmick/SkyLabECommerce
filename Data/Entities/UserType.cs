using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserType : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Type name is required!!")]
        [MaxLength(30)]
        public required string TypeName { get; set; }
        public ICollection<AdminUser> AdminUsers { get; } = new List<AdminUser>();
    }
}
