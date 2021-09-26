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
    public class NumberEntriesController : ControllerBase
    {
        private readonly INumberEntriesRepository _numberEntriesRepository;

        public NumberEntriesController(INumberEntriesRepository numberEntriesRepository)
        {
            _numberEntriesRepository = numberEntriesRepository;
        }

        [HttpGet("{dataPointId:int}")]
        public async Task<ActionResult<List<NumberEntriesDto>>> GetEntriesById(int dataPointId)
        {
            var result = await _numberEntriesRepository.GetEntriesById(dataPointId);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<NumberEntriesDto>> GetLastEntryById(int dataPointId)
        {
            var result = await _numberEntriesRepository.GetLastEntryById(dataPointId);
            if (result == null) return NoContent();
            return Ok(result);
        }
    }
}
