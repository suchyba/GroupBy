using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.FinancialOutcomeRecord
{
    public class FinancialOutcomeRecordCreateDTO
    {
        public decimal? Inventory { get; set; }
        public decimal? Material { get; set; }
        public decimal? Service { get; set; }
        public decimal? Transport { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Accommodation { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Food { get; set; }
        public decimal? Other { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
        public int BookOrderNumberId { get; set; }
        public int? RelatedProjectId { get; set; }
        public int RelatedDocumentId { get; set; }
    }
}
