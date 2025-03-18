namespace VF.Verify.Domain.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFullname { get; set; }
        public int DistributorId { get; set; }
        public bool IsDistributor { get; set; }
        public bool IsActive { get; set; }
    }
}
