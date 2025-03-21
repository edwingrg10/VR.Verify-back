using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.DTOs
{
    public class ProfileDetailDto
    {
        public int ProfileId { get; set; }
        public bool HasCriteria { get; set; }
        public List<ProfileSourceDto> Sources { get; set; }
    }
}
