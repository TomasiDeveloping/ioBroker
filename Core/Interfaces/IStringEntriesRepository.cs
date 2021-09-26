using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IStringEntriesRepository
    {
        public Task<List<StringEntriesDto>> GetEntriesById(int dataPointId);
        public Task<StringEntriesDto> GetLastEntryById(int dataPointId);
    }
}
