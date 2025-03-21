using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class ConsultationCriteria
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("source_id")]
        public int SourceId { get; set; }

        public virtual Source Source { get; set; }
        public virtual ICollection<VerificationField> VerificationFields { get; set; }
        public virtual ICollection<ProfileSource> ProfileSources { get; set; }

        public virtual ICollection<ConsultationCriteriaFields> CriteriaFields { get; set; }

    }
}
