using System.ComponentModel.DataAnnotations;

namespace VF.Verify.Domain.DTOs
{
    public class AssignPermissionsDTO
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public List<int> PermissionIds { get; set; }
    }
}
