using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels.Rank
{
    public class RankViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HigherRankId { get; set; }
    }
}
