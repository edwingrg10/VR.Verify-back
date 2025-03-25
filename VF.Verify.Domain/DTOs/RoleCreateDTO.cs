using System.ComponentModel.DataAnnotations;

namespace VF.Verify.Domain.DTOs
{
    public class RoleCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
