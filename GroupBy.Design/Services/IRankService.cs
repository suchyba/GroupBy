using GroupBy.Design.DTO.Rank;

namespace GroupBy.Design.Services
{
    public interface IRankService : IAsyncService<RankSimpleDTO, RankDTO, RankCreateDTO, RankSimpleDTO>
    {
    }
}
