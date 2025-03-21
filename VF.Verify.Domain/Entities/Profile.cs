using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ProfileSource> ProfileSources { get; set; }
    }
}
