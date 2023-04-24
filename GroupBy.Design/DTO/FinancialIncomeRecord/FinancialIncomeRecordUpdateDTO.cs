﻿namespace GroupBy.Design.TO.FinancialIncomeRecord
{
    public class FinancialIncomeRecordUpdateDTO
    {
        public Guid Id { get; set; }
        public decimal? MembershipFee { get; set; }
        public decimal? ProgramFee { get; set; }
        public decimal? Dotation { get; set; }
        public decimal? EarningAction { get; set; }
        public decimal? OnePercent { get; set; }
        public decimal? Other { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}