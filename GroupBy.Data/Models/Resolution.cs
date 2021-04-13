using System;

namespace GroupBy.Data.Models
{
    public class Resolution
    {
        // klucz
        public string Number { get; set; }
        public Group Group { get; set; }
        // klucz
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public Volunteer Legislator { get; set; }
    }
}