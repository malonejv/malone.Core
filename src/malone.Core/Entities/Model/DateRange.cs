﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace malone.Core.Entities.Model
{
    public class DateRange : IDateRange
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}