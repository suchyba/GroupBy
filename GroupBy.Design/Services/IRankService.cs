using GroupBy.Design.TO.Rank;

namespace GroupBy.Design.Services
{
    public interface IRankService : IAsyncService<RankSimpleDTO, RankDTO, RankCreateDTO, RankSimpleDTO>
    {
    }
}
