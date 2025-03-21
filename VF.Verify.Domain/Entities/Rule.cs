using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("internal_name")]
        public string InternalName { get; set; }

        // Foreign Key
        [Column("source_id")]
        public int SourceId { get; set; }

        // Navigation Property
        public virtual Source Source { get; set; }
    }
}
