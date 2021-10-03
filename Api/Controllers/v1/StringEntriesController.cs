using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DTO;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class StringEntriesController : ControllerBase
    {
        private readonly IStringEntriesRepository _stringEntriesRepository;

        public StringEntriesController(IStringEntriesRepository stringEntriesRepository)
        {
            _stringEntriesRepository = stringEntriesRepository;
        }

        [HttpGet("{dataPointId:int}")]
        public async Task<ActionResult<List<StringEntriesDto>>> GetEntriesById(int dataPointId)
        {
            var result = await _stringEntriesRepository.GetEntriesByIdAsync(dataPointId);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<StringEntriesDto>> GetLastEntryById(int dataPointId)
        {
            var result = await _stringEntriesRepository.GetLastEntryByIdAsync(dataPointId);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<List<StringEntriesDto>>> GetEntriesByIdAndParams(int dataPointId,
            [FromQuery] int pageSize)
        {
            var result = await _stringEntriesRepository.GetEntriesByIdAndParamsAsync(dataPointId, pageSize);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }
    }
}
