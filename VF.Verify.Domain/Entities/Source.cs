using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("base_url")]
        public string BaseUrl { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        // Foreign Keys

        [Column("entity_id")]
        public int EntityId { get; set; }

        [Column("extraction_method_id")]
        public int ExtractionMethodId { get; set; }

        // Navigation Properties
        public virtual Entity Entity { get; set; }
        public virtual ExtractionMethod ExtractionMethod { get; set; }
        public virtual ICollection<ConsultationCriteria> ConsultationCriterias { get; set; }
        public virtual ICollection<VerificationField> VerificationFields { get; set; }
        public virtual ICollection<CompanySources> CompanySources { get; set; }
        public virtual ICollection<ProfileSource> ProfileSources { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
        
    }
}
