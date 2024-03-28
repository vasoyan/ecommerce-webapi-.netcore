using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;
    }
}
