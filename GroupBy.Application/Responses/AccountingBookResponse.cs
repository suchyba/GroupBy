using GroupBy.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Responses
{
    public class AccountingBookResponse : BaseResponse
    {
        public AccountingBookViewModel accountingBook { get; set; }
    }
}
