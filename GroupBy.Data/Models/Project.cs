using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Volunteer Owner { get; set; }
        public Group ProjectGroup { get; set; }
        public Group ParentGroup { get; set; }
        public bool Active { get; set; }
        public bool Independent { get; set; }
        public IEnumerable<FinancialRecord> AssignedFinnancialRecords { get; set; }
        public IEnumerable<Elements> Elements { get; set; }
    }
}
