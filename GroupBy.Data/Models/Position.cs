﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Data.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Position HigherPosition { get; set; }
    }
}
