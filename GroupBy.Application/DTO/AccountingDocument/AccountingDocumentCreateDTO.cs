using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.AccountingDocument
{
    public class AccountingDocumentCreateDTO
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public int GroupId { get; set; }
        public int? ProjectId { get; set; }
    }
}
