using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class ExtractionMethod
    {
        public int Id { get; set; }
        public ExtractionMethodType Type { get; set; }

        [Column("internal_name")]
        public string InternalName { get; set; }

        // Navigation Property
        public virtual ICollection<Source> Sources { get; set; }
    }

    public enum ExtractionMethodType
    {
        BOT,
        API
    }
}
