using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Rank
{
    public class RankDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RankSimpleDTO HigherRank { get; set; }
    }
}
