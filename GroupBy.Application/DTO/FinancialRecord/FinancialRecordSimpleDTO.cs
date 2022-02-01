using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.FinancialRecord
{
    public class FinancialRecordSimpleDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string RelatedDocumentName { get; set; }
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? OtherIncome { get; set; }

        public decimal? Inventory { get; set; }
        public decimal? Material { get; set; }
        public decimal? Service { get; set; }
        public decimal? Transport { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Accommodation { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Food { get; set; }
        public decimal? OtherOutcome { get; set; }
        public decimal Total { get; set; }
    }
}
