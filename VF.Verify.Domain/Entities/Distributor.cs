using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VF.Verify.Domain.Entities
{
    public class Distributor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        [Column("primary_color")]
        public string PrimaryColor { get; set; }

        [Column("secondary_color")]
        public string SecondaryColor { get; set; }
        
        [JsonIgnore]
        public ICollection<Company> Companies { get; set; } // relacion con company
    }
}
