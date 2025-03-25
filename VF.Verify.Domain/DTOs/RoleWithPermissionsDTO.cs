namespace VF.Verify.Domain.DTOs
{
    public class RoleWithPermissionsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PermissionDTO> Permissions { get; set; }
    }
}
