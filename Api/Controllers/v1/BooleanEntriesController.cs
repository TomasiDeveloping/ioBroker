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
    public class BooleanEntriesController : ControllerBase
    {
        private readonly IBooleanEntriesRepository _booleanEntriesRepository;

        public BooleanEntriesController(IBooleanEntriesRepository booleanEntriesRepository)
        {
            _booleanEntriesRepository = booleanEntriesRepository;
        }

        [HttpGet("{dataPointId:int}")]
        public async Task<ActionResult<List<BooleanEntriesDto>>> GetEntriesById(int dataPointId)
        {
            var result = await _booleanEntriesRepository.GetEntriesById(dataPointId);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<BooleanEntriesDto>> GetLastEntryById(int dataPointId)
        {
            var result = await _booleanEntriesRepository.GetLastEntryById(dataPointId);
            if (result == null) return NoContent();
            return Ok(result);
        }
    }
}
