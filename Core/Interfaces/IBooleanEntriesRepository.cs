using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IBooleanEntriesRepository
    {
        public Task<List<BooleanEntriesDto>> GetEntriesById(int dataPointId);
        public Task<BooleanEntriesDto> GetLastEntryById(int dataPointId);
    }
}
