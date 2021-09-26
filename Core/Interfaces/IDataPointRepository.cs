using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;

namespace Core.Interfaces
{
    public interface IDataPointRepository
    {
        public Task<List<DataPointDto>> GetDataPointsAsync();
        public Task<DataPointDto> GetDataPointByIdAsync(int dataPointId);
        public Task<List<DataPointDto>> GetDataPointsByNameAsync(string name);
    }
}
