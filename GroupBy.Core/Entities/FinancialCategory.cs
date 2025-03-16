using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupBy.Domain.Entities
{
    public class FinancialCategory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Income { get; set; }
        public virtual IEnumerable<AccountingBook> AccountingBooks { get; set; }
        public virtual IEnumerable<AccountingBookTemplate> AccountingBookTemplates { get; set; }
    }
}
