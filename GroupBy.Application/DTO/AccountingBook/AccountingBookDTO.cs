using GroupBy.Application.DTO.FinancialIncomeRecord;
using GroupBy.Application.DTO.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.AccountingBook
{
    public class AccountingBookDTO
    {
        public int BookId { get; set; }
        public int BookOrderNumberId { get; set; }
        public string Name { get; set; }
        public bool Locked { get; set; }
        public GroupSimpleDTO RelatedGroup { get; set; }
        public decimal Balance { get; set; }
    }
}
