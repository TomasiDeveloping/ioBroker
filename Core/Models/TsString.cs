using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TsString
    {
        public int? Id { get; set; }
        public long? Ts { get; set; }
        public string? Val { get; set; }
        public bool? Ack { get; set; }
        public int? From { get; set; }
        public int? Q { get; set; }
    }
}
