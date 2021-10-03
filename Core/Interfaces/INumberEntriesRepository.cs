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
        public Task<List<NumberEntriesDto>> GetEntriesByIdAndParam(int dataPointId, int pageSize);
        public Task<NumberEntriesDto> GetLastEntryById(int dataPointId);
        public Task<NumberEntriesDto> UpdateEntry(NumberEntriesDto numberEntriesDto);
        public Task<bool> DeleteEntryByIdAndTimeStamp(int dataPointId, DateTime timeStamp);
    }
}
