using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.RegistrationCode
{
    public class RegistrationCodeCreateDTO
    {
        public string Name { get; set; }
        public int TargetGroupId { get; set; }
        public int? TargetRankId { get; set; }
        public int OwnerId { get; set; }
    }
}
