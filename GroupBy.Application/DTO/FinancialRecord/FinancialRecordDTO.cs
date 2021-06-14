﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.FinancialRecord
{
    public class FinancialRecordDTO
    {
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string RelatedDocumentName { get; set; }
    }
}