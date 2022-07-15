using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.RegistrationCode
{
    public class RegistrationCodeListDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TargetGroupName { get; set; }
        public string TargetRankName { get; set; }
    }
}
