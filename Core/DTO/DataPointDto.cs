using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class DataPointDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? Type { get; set; }

        public string? TypeName { get; set; }
    }
}
