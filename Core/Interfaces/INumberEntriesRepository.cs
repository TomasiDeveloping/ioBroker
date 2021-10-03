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
        public Task<List<NumberEntriesDto>> GetEntriesByIdAsync(int dataPointId);
        public Task<List<NumberEntriesDto>> GetEntriesByIdAndParamAsync(int dataPointId, int pageSize);
        public Task<NumberEntriesDto> GetLastEntryByIdAsync(int dataPointId);
        public Task<NumberEntriesDto> UpdateEntryAsync(NumberEntriesDto numberEntriesDto);
        public Task<bool> DeleteEntryByIdAndTimeStampAsync(int dataPointId, DateTime timeStamp);
    }
}
