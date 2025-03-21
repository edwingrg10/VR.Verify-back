using System.ComponentModel.DataAnnotations.Schema;

namespace VF.Verify.Domain.Entities
{
    public class CompanyCountry
    {
        public int Id { get; set; }

        [Column("country_id")]
        public int CountryId { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual Company Company { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<CompanySources> CompanySources { get; set; }


    }

}
