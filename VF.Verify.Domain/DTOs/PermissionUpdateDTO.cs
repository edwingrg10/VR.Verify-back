using System.ComponentModel.DataAnnotations;

namespace VF.Verify.Domain.DTOs
{
    public class PermissionUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
