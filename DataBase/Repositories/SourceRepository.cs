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
    public class SourceRepository : ISourceRepository
    {
        private readonly IoBrokerContext _context;

        public SourceRepository(IoBrokerContext context)
        {
            _context = context;
        }
        public async Task<SourceDto> GetSourceByIdAsync(int sourceId)
        {
            var source = await _context.Sources
                .Select(s => new SourceDto()
                {
                    Id = s.Id,
                    Adapter = s.Name
                })
                .FirstOrDefaultAsync(s => s.Id == sourceId);

            if (source == null) return null;
            if (string.IsNullOrEmpty(source.Adapter)) return source;

            var nameSplit = source.Adapter.Split('.');
            source.Adapter = nameSplit[2];
            if (nameSplit.Count() > 3) source.Adapter = string.Concat(source.Adapter, ".", nameSplit[3]);

            return source;
        }

        public async Task<List<SourceDto>> GetSourcesAsync()
        {
            var sources = await _context.Sources
                .Select(s => new SourceDto()
                {
                    Id = s.Id,
                    Adapter = s.Name
                })
                .ToListAsync();
            if (!sources.Any()) return sources;
            foreach (var source in sources)
            {
                if (string.IsNullOrEmpty(source.Adapter)) continue;
                var nameSplit = source.Adapter.Split('.');
                source.Adapter = nameSplit[2];
                if (nameSplit.Count() > 3) source.Adapter = string.Concat(source.Adapter, ".", nameSplit[3]);
            }

            return sources;
        }
    }
}
