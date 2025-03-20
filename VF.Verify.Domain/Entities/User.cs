using System.ComponentModel.DataAnnotations.Schema;

namespace VF.Verify.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }

        [Column("rol_id")]
        public int RolId { get; set; }

        [Column("distributor_id")]
        public int DistributorId { get; set; }

        [Column("company_country_id")]
        public int CompanyCountryId { get; set; }

        // relaciones
        public virtual Role? Role { get; set; }
        public virtual Distributor? Distributor { get; set; }
        public virtual CompanyCountry? CompanyCountry { get; set; }
    }

}
