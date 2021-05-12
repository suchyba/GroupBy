﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.ViewModels
{
    public class VolunteerViewModel
    {
        public int Id { get; set; }
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool Confirmed { get; set; }
        public string RankName { get; set; }
    }
}
