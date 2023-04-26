namespace GroupBy.Design.DTO.Rank
{
    public class RankSimpleDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? HigherRankId { get; set; }
    }
}
