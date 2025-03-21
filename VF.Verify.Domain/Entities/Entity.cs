using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // Foreign Key
        [Column("country_id")]
        public int CountryId { get; set; }

        // Navigation Properties
        public virtual Country Country { get; set; }
        public virtual ICollection<Source> Sources { get; set; }
    }
}
