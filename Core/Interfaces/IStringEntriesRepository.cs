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
        public Task<List<StringEntriesDto>> GetEntriesByIdAsync(int dataPointId);
        public Task<List<StringEntriesDto>> GetEntriesByIdAndParamsAsync(int dataPointId, int pageSize); 
        public Task<StringEntriesDto> GetLastEntryByIdAsync(int dataPointId);
    }
}
