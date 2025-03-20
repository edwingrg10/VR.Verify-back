using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Nit { get; set; }
        public string Name { get; set; }

        [Column("contact_email")]
        public string ContactEmail { get; set; }

        [Column("contact_fullname")]
        public string ContactFullName { get; set; }

        [Column("distributor_id")]
        public int DistributorId { get; set; }

        [Column("is_distributor")]
        public bool IsDistributor { get; set; } = false;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        public Distributor Distributor { get; set; } // relacion con distribuitor
        public ICollection<CompanyCountry> CompanyCountries { get; set; } = new List<CompanyCountry>(); // relacion uno a muchos con CompanyCountry

    }
}
