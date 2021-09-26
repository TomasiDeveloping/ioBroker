using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories
{
    public class StringEntriesRepository : IStringEntriesRepository
    {
        private readonly IoBrokerContext _context;

        public StringEntriesRepository(IoBrokerContext context)
        {
            _context = context;
        }
        public async Task<List<StringEntriesDto>> GetEntriesById(int dataPointId)
        {
            return await GetEntriesByParams(dataPointId);
        }

        public async Task<StringEntriesDto> GetLastEntryById(int dataPointId)
        {
            var result = await GetEntriesByParams(dataPointId, 1);
            return result.FirstOrDefault(i => i.Id == dataPointId);
        }

        private async Task<List<StringEntriesDto>> GetEntriesByParams(int id = 0, int take = 0)
        {
            var entries = new List<StringEntriesDto>();

            var query = _context.TsStrings
                .Join(
                    _context.Datapoints,
                    number => number.Id,
                    dataPoint => dataPoint.Id,
                    (number, dataPoint) => new { number, dataPoint }
                )
                .Join(
                    _context.Sources,
                    n => n.number.From,
                    source => source.Id,
                    (n, source) => new
                    {
                        Id = n.number.Id,
                        DataPoint = n.dataPoint.Name,
                        AdapterName = source.Name,
                        Adapter = source.Id,
                        Confirmation = n.number.Ack,
                        Quality = n.number.Q.Value,
                        Value = n.number.Val,
                        TimeStamp = n.number.Ts.Value
                    }
                );
            query = query.OrderByDescending(t => t.TimeStamp);

            if (id > 0) query = query.Where(i => i.Id == id);

            if (take > 0) query = query.Take(take);

            await query.ToListAsync();

            foreach (var entry in query)
            {
                entries.Add(new StringEntriesDto()
                {
                    Id = entry.Id,
                    DataPointName = entry.DataPoint ?? "Undefined",
                    Adapter = entry.Adapter,
                    AdapterName = entry.AdapterName ?? "Undefined",
                    Confirmation = entry.Confirmation,
                    Quality = entry.Quality,
                    Value = entry.Value,
                    TimeStamp = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(entry.TimeStamp).ToLocalTime()
                });
            }
            return entries;
        }
    }
}
