using GroupBy.Design.DTO.Group;
using GroupBy.Design.DTO.Rank;
using GroupBy.Design.DTO.Volunteer;

namespace GroupBy.Design.DTO.RegistrationCode
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
