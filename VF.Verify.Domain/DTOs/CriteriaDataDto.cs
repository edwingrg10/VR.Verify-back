using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VF.Verify.Domain.DTOs
{
    public class CriteriaDataDto
    {
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; }
        public List<FieldDto> Fields { get; set; }
    }
}
