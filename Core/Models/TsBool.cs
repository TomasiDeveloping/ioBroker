﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TsBool
    {
        public int? Id { get; set; }
        public long? Ts { get; set; }
        public bool? Val { get; set; }
        public bool? Ack { get; set; }
        public int? From { get; set; }
        public int? Q { get; set; }
    }
}
