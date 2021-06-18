﻿using GroupBy.Application.DTO.InventoryBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Design.Services
{
    public interface IInventoryBookService : IAsyncService<InventoryBookDTO, InventoryBookCreateDTO, InventoryBookUpdateDTO>
    {

    }
}
