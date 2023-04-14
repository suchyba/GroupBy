namespace GroupBy.Design.TO.Rank
{
    public class RankSimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? HigherRankId { get; set; }
    }
}
