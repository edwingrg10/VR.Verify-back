using System.ComponentModel.DataAnnotations;

namespace VF.Verify.Domain.DTOs
{
    public class PermissionCreateDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
