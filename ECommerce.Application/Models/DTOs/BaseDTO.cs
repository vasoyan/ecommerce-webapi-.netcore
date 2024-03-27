using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models.DTOs
{
    public class BaseDTO
    {
        [Key]
        public int Id { get; set; }
    }
}
