using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Rank HigherRank { get; set; }
    }
}