using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.RegistrationCode
{
    public class RegistrationCodeUpdateDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TargetGroupId { get; set; }
        public int? TargetRankId { get; set; }
    }
}
