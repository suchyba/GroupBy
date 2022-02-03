﻿using GroupBy.Application.DTO.Group;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.DTO.AccountingDocument
{
    public class AccountingDocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public GroupSimpleDTO Group { get; set; }
        public ProjectSimpleDTO RelatedProject { get; set; }
    }
}
