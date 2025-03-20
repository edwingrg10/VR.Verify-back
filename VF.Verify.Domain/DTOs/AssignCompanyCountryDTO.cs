namespace VF.Verify.Domain.DTOs
{
    public class AssignCompanyCountryDTO
    {
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
