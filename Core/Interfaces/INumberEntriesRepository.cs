using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface INumberEntriesRepository
    {
        public Task<List<NumberEntriesDto>> GetEntriesById(int dataPointId);
        public Task<NumberEntriesDto> GetLastEntryById(int dataPointId);
    }
}
