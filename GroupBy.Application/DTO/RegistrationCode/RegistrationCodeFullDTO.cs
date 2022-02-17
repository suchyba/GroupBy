using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Rank;
using GroupBy.Application.DTO.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.RegistrationCode
{
    public class RegistrationCodeFullDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public GroupSimpleDTO TargetGroup { get; set; }
        public RankSimpleDTO TargetRank { get; set; }
        public VolunteerSimpleDTO Owner { get; set; }
    }
}
