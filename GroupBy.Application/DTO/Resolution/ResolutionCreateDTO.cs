using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Resolution
{
    public class ResolutionCreateDTO
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int LegislatorId { get; set; }
        public int GroupId { get; set; }
    }
}
