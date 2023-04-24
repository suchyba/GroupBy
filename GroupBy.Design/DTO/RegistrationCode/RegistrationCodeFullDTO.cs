using GroupBy.Design.TO.Group;
using GroupBy.Design.TO.Rank;
using GroupBy.Design.TO.Volunteer;

namespace GroupBy.Design.TO.RegistrationCode
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
