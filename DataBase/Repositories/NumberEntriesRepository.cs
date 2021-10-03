using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repositories
{
    public class NumberEntriesRepository : INumberEntriesRepository
    {
        private readonly IoBrokerContext _context;

        public NumberEntriesRepository(IoBrokerContext context)
        {
            _context = context;
        }
        public async Task<List<NumberEntriesDto>> GetEntriesById(int dataPointId)
        {
            return await GetEntriesByParams(dataPointId);
        }

        public async Task<List<NumberEntriesDto>> GetEntriesByIdAndParam(int dataPointId, int pageSize)
        {
            var result = await GetEntriesByParams(dataPointId, pageSize);
            return result;
        }

        public async Task<NumberEntriesDto> GetLastEntryById(int dataPointId)
        {
            var result = await GetEntriesByParams(dataPointId, 1);
            return result.FirstOrDefault(i => i.Id == dataPointId);
        }

        public async Task<NumberEntriesDto> UpdateEntry(NumberEntriesDto numberEntriesDto)
        {
            var timeStamp = (long) numberEntriesDto.TimeStamp.ToUniversalTime().Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            ).TotalMilliseconds;

            var entryToUpdate = await _context.TsNumbers.FirstOrDefaultAsync(n =>
                n.Id == numberEntriesDto.Id && n.Ts == timeStamp);
            if (entryToUpdate == null) return null;

            await _context.Database.ExecuteSqlInterpolatedAsync($"UPDATE [ioBroker].[dbo].[ts_number] SET val = {numberEntriesDto.Value} WHERE id = {numberEntriesDto.Id} AND ts = {timeStamp}");
            return numberEntriesDto;
        }

        public async Task<bool> DeleteEntryByIdAndTimeStamp(int dataPointId, DateTime timeStamp)
        {
            var ts = (long) timeStamp.ToUniversalTime().Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            ).TotalMilliseconds;

            var entryToDelete = await _context.TsNumbers.FirstOrDefaultAsync(n => n.Id == dataPointId && n.Ts == ts);
            if (entryToDelete == null) return false;

            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [ioBroker].[dbo].[ts_number] WHERE id ={dataPointId} AND ts={ts}");
            return true;
        }

        private async Task<List<NumberEntriesDto>> GetEntriesByParams(int id = 0, int take = 0)
        {
            var entries = new List<NumberEntriesDto>();

            var query = _context.TsNumbers
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
                        Value = n.number.Val.Value,
                        TimeStamp = n.number.Ts.Value
                    }
                );
            query = query.OrderByDescending(t => t.TimeStamp);

            if (id > 0) query = query.Where(i => i.Id == id);

            if (take > 0) query = query.Take(take);

            await query.ToListAsync();

            foreach (var entry in query)
            {
                entries.Add(new NumberEntriesDto()
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
