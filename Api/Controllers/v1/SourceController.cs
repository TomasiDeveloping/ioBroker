using System.Collections.Generic;
using System.Linq;
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
    public class SourceController : ControllerBase
    {
        private readonly ISourceRepository _sourceRepository;

        public SourceController(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SourceDto>>> GetSources()
        {
            var result = await _sourceRepository.GetSourcesAsync();
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("{sourceId:int}")]
        public async Task<ActionResult<SourceDto>> GetSourceById(int sourceId)
        {
            var result = await _sourceRepository.GetSourceByIdAsync(sourceId);
            if (result == null) return NoContent();

            return Ok(result);
        }
    }
}
