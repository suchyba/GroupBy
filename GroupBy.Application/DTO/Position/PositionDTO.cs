using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.Position
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? HigherPositionId { get; set; }
    }
}
