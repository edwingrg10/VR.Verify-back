namespace VF.Verify.Domain.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int DistributorId { get; set; }
        public int CompanyCountryId { get; set; }
    }
}
