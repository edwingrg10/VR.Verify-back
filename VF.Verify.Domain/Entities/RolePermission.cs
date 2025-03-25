using System.ComponentModel.DataAnnotations.Schema;

namespace VF.Verify.Domain.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
