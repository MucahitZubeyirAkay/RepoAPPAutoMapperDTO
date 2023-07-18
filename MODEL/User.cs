using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

      
        [Required]
        public string UserPassword { get; set; }

        public string? Email { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public string Role { get; set; } = "user";
    }
}
