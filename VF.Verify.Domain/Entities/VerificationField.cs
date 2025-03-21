using System.ComponentModel.DataAnnotations.Schema;

namespace VF.Verify.Domain.Entities
{
    public class VerificationField
    {
        public int Id { get; set; }

        [Column("consultation_criteria_id")]
        public int? ConsultationCriteriaId { get; set; }

        [Column("source_id")]
        public int SourceId { get; set; }

        [Column("field_id")]
        public int FieldId { get; set; }

        [Column("is_required")]
        public bool IsRequired { get; set; }

        public virtual ConsultationCriteria? ConsultationCriteria { get; set; }
        public virtual Source Source { get; set; }
        public virtual Field Field { get; set; }
    }
}
