using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Rank
{
    public class RankCreateDTO
    {
        public string Name { get; set; }
        public int? HigherRankId { get; set; }
    }
}
