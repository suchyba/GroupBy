using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Type representing project which is organised by group <see cref="ParentGroup"/>
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Identificator of the project
        /// </summary>
        [Key]
        public int Id { get; set; }
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
        public int? ProjectGroupId { get; set; }
        /// <summary>
        /// If the group of target volunteers is diffrent than the group which is organising the project (<see cref="ProjectGroup"/>) or you want to set up the independent project, you could set this property <seealso cref="Group"/>
        /// </summary>
        [ForeignKey("ProjectGroupId")]
        public virtual Group ProjectGroup { get; set; }
        /// <summary>
        /// Group which is organising this project <seealso cref="Group"/>
        /// </summary>
        [Required]
        public virtual Group ParentGroup { get; set; }
        /// <summary>
        /// Describes if the project is still in progress
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Describes if the project is the inedpendent project (has its own accounting book <see cref="ProjectGroup"/>)
        /// </summary>
        public bool Independent { get; set; }
        /// <summary>
        /// The list of financial records related with this project <seealso cref="FinancialRecord"/>
        /// </summary>
        public virtual IEnumerable<FinancialRecord> RelatedFinnancialRecords { get; set; }
    }
}
