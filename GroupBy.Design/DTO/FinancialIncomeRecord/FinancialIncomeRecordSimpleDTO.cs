﻿using GroupBy.Design.DTO.FinancialCategoryValue;

namespace GroupBy.Design.DTO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordSimpleDTO
    {
        public Guid Id { get; set; }        
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public IEnumerable<FinancialCategoryValueSimpleDTO> Values { get; set; }
    }
}
