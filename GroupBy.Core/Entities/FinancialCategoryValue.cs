using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupBy.Domain.Entities
{
    public class FinancialCategoryValue
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public FinancialCategory Category { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }
    }
}
