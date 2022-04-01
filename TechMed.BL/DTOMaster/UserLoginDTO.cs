using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class UserLoginDTO
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HashPassword { get; set; }      
        public int? LoginAttempts { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool? IsPasswordChanged { get; set; }
        public bool IsActive { get; set; }
    }
}
