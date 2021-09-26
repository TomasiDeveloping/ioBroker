using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class CounterDto
    {
        public int? Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public float? Value { get; set; }
    }
}
