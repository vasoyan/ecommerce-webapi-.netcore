using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Models.VMs
{
    public class BaseViewModel
    {
        [Key]
        public int Id { get; set; }
    }
}
