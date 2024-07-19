using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace forceget.DataAccess.Models.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "SecondName is required")]
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public int Age { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}