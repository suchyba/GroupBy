using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordUpdateDTO
    {
        public int Id { get; set; }
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? Other { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? RelatedProjectId { get; set; }
    }
}
