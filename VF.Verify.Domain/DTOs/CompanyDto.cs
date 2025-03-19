namespace VF.Verify.Domain.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFullName { get; set; }
        public int DistributorId { get; set; }
        public bool IsDistributor { get; set; }
        public bool IsActive { get; set; }
        public DistributorDto Distributor { get; set; }
    }

    public class CreateCompanyDTO
    {
        public string Nit { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFullName { get; set; }
        public int DistributorId { get; set; }
        public bool IsDistributor { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }

    public class UpdateCompanyDTO
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFullName { get; set; }
        public int DistributorId { get; set; }
        public bool IsDistributor { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}
