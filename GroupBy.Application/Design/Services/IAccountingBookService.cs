﻿using GroupBy.Application.DTO.AccountingBook;

namespace GroupBy.Application.Design.Services
{
    public interface IAccountingBookService : IAsyncService<AccountingBookDTO, AccountingBookDTO, AccountingBookCreateDTO, AccountingBookDTO>
    {

    }
}
