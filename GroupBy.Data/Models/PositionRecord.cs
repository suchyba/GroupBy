using System;

namespace GroupBy.Data.Models
{
    public class PositionRecord
    {
        public Position Position { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? DismissDate { get; set; }
        // klucz
        public Volunteer Person { get; set; }
        public int Id { get; set; }
        // klucz
        public Resolution AppointingResolution { get; set; }
        public Resolution DismissingResolution { get; set; }
        public Group RelatedGroup { get; set; }
    }
}