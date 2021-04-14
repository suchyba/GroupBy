using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    public class RegistrationCode
    {
        [Key]
        public string Key { get; set; }
        public string Name { get; set; }
        public Group TargetGroup { get; set; }
        public Position TargetPosition { get; set; }
        public Rank TargetRank { get; set; }
    }
}
