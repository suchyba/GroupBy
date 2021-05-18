using GroupBy.Application.ViewModels.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IRankService : IAsyncService<RankViewModel, RankCreateViewModel, RankViewModel>
    {
    }
}
