using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
        public string? Metadata { get; set; } // JSON

        // Navigation Properties
        public virtual ICollection<ConsultationCriteriaFields> CriteriaFields { get; set; }
        public virtual ICollection<VerificationField> VerificationFields { get; set; }
    }

    public enum FieldType
    {
        TEXT,
        LIST,
        NUMBER,
        DATE
    }
}
