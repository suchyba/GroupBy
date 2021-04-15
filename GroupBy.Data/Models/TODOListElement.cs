using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    public class TODOListElement
    {
        [Key]
        public int Id { get; set; }
        public string Task { get; set; }
        public virtual IEnumerable<TODOListElement> SubTasks { get; set; }
        public virtual Volunteer Asignee { get; set; }
        public DateTime? Deadline { get; set; }
    }
}