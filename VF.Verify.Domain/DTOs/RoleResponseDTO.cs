namespace VF.Verify.Domain.DTOs
{
    public class RoleResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}
