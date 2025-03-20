namespace VF.Verify.Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public int DistributorId { get; set; }
        public int CompanyCountryId { get; set; }
    }
}
