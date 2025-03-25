namespace VF.Verify.Domain.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RolePermissionDTO> Permissions { get; set; }
    }
}
