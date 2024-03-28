using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models.DTOs;

public partial class UserDTO : BaseDTO
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    private string _passwordHash;
    [Required]
    public string PasswordHash
    {
        get => _passwordHash;
        set
        {
            _passwordHash = BCrypt.Net.BCrypt.HashPassword(value);
        }
    }

    public int RoleId { get; set; }
    
    // bool isPasswordValid = BCrypt.Net.BCrypt.Verify(enteredPassword, storedEncryptedPassword);

    // public string JwtToken { get; set; } = null!;

    //public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    // public virtual RefreshTokenDTO? RefreshToken { get; set; }

    // public virtual RoleDTO Role { get; set; } = null!;
}
