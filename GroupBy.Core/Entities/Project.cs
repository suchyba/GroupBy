﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Type representing project which is organized by group <see cref="ParentGroup"/>
    /// </summary>
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of the project
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Description of the project
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Date when this project starts
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// Date when this project ends
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Volunteer who owns this project <seealso cref="Volunteer"/>
        /// </summary>
        [Required]
        public virtual Volunteer Owner { get; set; }
        public Guid? ProjectGroupId { get; set; }
        /// <summary>
        /// If the group of target volunteers is different than the group which is organizing the project (<see cref="ProjectGroup"/>) or you want to set up the independent project, you could set this property <seealso cref="Group"/>
        /// </summary>
        [ForeignKey("ProjectGroupId")]
        public virtual Group ProjectGroup { get; set; }
        /// <summary>
        /// Groups which is organizing this project <seealso cref="Group"/>
        /// </summary>
        [Required]
        public virtual Group ParentGroup { get; set; }
        /// <summary>
        /// Describes if the project is still in progress
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Describes if the project is the independent project (has its own accounting book <see cref="ProjectGroup"/>)
        /// </summary>
        public bool Independent { get; set; }
        /// <summary>
        /// The list of financial records related with this project <seealso cref="FinancialRecord"/>
        /// </summary>
        public virtual IEnumerable<FinancialRecord> RelatedFinnancialRecords { get; set; }
        /// <summary>
        /// The list of elements related with this project <seealso cref="Element"/>
        /// </summary>
        public virtual IEnumerable<Element> RelatedElements { get; set; }
    }
}
