﻿using System;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// The resolution adopted in the group <see cref="Group"/>
    /// </summary>
    public class Resolution
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// The identification symbol of the resolution
        /// </summary>
        [Required]
        public string Symbol { get; set; }
        /// <summary>
        /// Date when the resolution has been adopted
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The content of the resolution
        /// </summary>
        [Required]
        public string Content { get; set; }
        /// <summary>
        /// The legislator of this resolution <seealso cref="Volunteer"/>
        /// </summary>
        [Required]
        public virtual Volunteer Legislator { get; set; }
        /// <summary>
        /// Groups where that resolution has been adopted <seealso cref="Group"/>
        /// </summary>
        [Required]
        public virtual Group Group { get; set; }
    }
}