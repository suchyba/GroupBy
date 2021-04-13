using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class Volunteer
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        [Key]
        public string Id { get; set; }
        public Position GetPosition()
        {
            return PositionHistory.Where(h => h.DismissDate == null).FirstOrDefault()?.Position;
        }
        public IEnumerable<PositionRecord> PositionHistory { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public IEnumerable<Project> OwnedProjects { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
