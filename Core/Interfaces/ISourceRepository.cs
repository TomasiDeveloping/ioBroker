using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface ISourceRepository
    {
        public Task<List<SourceDto>> GetSourcesAsync();
        public Task<SourceDto> GetSourceByIdAsync(int sourceId);
    }
}
