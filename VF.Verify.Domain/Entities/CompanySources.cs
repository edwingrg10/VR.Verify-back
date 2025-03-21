using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class CompanySources
    {
        public int Id { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // Foreign Keys

        [Column("source_id")]
        public int SourceId { get; set; }

        [Column("company_country_id")]
        public int CompanyCountryId { get; set; }

        // Navigation Properties
        public virtual Source Source { get; set; }
        public virtual CompanyCountry CompanyCountry { get; set; }
    }
}
