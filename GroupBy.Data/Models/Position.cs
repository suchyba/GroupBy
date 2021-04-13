using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Poprzednia w hierarchii")]
        public Position HigherRole { get; set; }
    }
}
