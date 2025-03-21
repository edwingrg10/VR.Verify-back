using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class ConsultationCriteriaFields
    {
        public int Id { get; set; }

        [Column("is_required")]
        public bool IsRequired { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
        public string Value { get; set; }

        // Foreign Keys

        [Column("consultation_criteria_id")]
        public int ConsultationCriteriaId { get; set; }

        [Column("field_id")]
        public int FieldId { get; set; }

        // Navigation Properties
        public virtual ConsultationCriteria ConsultationCriteria { get; set; }
        public virtual Field Field { get; set; }
    }
}
