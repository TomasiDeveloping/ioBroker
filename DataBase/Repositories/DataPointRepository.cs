using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories
{
    public class DataPointRepository : IDataPointRepository
    {
        private readonly IoBrokerContext _context;

        public DataPointRepository(IoBrokerContext context)
        {
            _context = context;
        }

        public async Task<List<DataPointDto>> GetDataPointsAsync()
        {
            var dataPoints = await _context.Datapoints
                .Select(d => new DataPointDto()
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.Type
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            if (dataPoints.Count <= 0) return dataPoints;

            foreach (var dataPoint in dataPoints)
            {
                dataPoint.TypeName = GetTypeNames(dataPoint.Type ?? 4);
            }

            return dataPoints;
        }

        public async Task<DataPointDto> GetDataPointByIdAsync(int dataPointId)
        {
            var dataPoint = await _context.Datapoints
                .FirstOrDefaultAsync(d => d.Id == dataPointId);

            if (dataPoint != null)
            {
                return new DataPointDto()
                {
                    Id = dataPoint.Id,
                    Type = dataPoint.Type,
                    Name = dataPoint.Name,
                    TypeName = GetTypeNames(dataPoint.Type ?? 4)
                };
            }

            return null;
        }

        public async Task<List<DataPointDto>> GetDataPointsByNameAsync(string name)
        {
            var dataPointsDto = new List<DataPointDto>();
            var dataPoints = await _context.Datapoints
                .Where(d => d.Name != null && d.Name.Contains(name))
                .ToListAsync();
            if (dataPoints.Count <= 0) return dataPointsDto;

            dataPointsDto.AddRange(dataPoints
                .Select(dataPoint => new DataPointDto() 
                    { Id = dataPoint.Id, 
                        Type = dataPoint.Type, 
                        Name = dataPoint.Name, 
                        TypeName = GetTypeNames(dataPoint.Type ?? 4)
                    }));

            return dataPointsDto;

        }


        private static string GetTypeNames(int type)
        {
            return type switch
            {
                0 => "Number",
                1 => "String",
                2 => "Boolean",
                _ => "Undefined"
            };
        }
    }
}
