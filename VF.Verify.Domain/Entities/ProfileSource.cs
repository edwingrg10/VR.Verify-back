using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VF.Verify.Domain.Entities
{
    public class ProfileSource
    {
        public int Id { get; set; }

        // Foreign Keys

        [Column("profile_id")]
        public int ProfileId { get; set; }

        [Column("source_id")]
        public int SourceId { get; set; }

        [Column("consultation_criteria_id")]
        public int? ConsultationCriteriaId { get; set; }

        // Navigation Properties
        public virtual Profile Profile { get; set; }
        public virtual Source Source { get; set; }
        public virtual ConsultationCriteria ConsultationCriteria { get; set; }
    }
}
