using System.ComponentModel.DataAnnotations;

namespace ECommerce.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
