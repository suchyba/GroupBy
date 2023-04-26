namespace GroupBy.Design.DTO.Rank
{
    public class RankDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RankSimpleDTO HigherRank { get; set; }
    }
}
