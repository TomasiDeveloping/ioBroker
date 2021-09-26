using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class Entries
    {
        public int? Id { get; set; }
        public string DataPointName { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool? Confirmation { get; set; }
        public int? Adapter { get; set; }
        public string AdapterName { get; set; }
        public int? Quality { get; set; }
    }
}
