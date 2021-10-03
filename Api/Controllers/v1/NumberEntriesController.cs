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
            var result = await _numberEntriesRepository.GetEntriesByIdAsync(dataPointId);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<List<NumberEntriesDto>>> GetEntriesByIdAndParams(int dataPointId,
            [FromQuery] int pageSize)
        {
            var result = await _numberEntriesRepository.GetEntriesByIdAndParamAsync(dataPointId, pageSize);
            if (result.Count <= 0) return NoContent();
            return Ok(result);
        }

        [HttpGet("[action]/{dataPointId:int}")]
        public async Task<ActionResult<NumberEntriesDto>> GetLastEntryById(int dataPointId)
        {
            var result = await _numberEntriesRepository.GetLastEntryByIdAsync(dataPointId);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpPut("{dataPointId:int}")]
        public async Task<ActionResult<NumberEntriesDto>> UpdateEntry(int dataPointId,
            NumberEntriesDto numberEntriesDto)
        {
            if (numberEntriesDto.Id != dataPointId) return BadRequest("Error with dataPointId");
            var result = await _numberEntriesRepository.UpdateEntryAsync(numberEntriesDto);
            if (result == null) return BadRequest("Konnte nicht bearbeitet werden");
            return Ok(result);
        }

        [HttpDelete("{dataPointId:int}")]
        public async Task<ActionResult<bool>> DeleteEntry(int dataPointId, NumberEntriesDto numberEntriesDto)
        {
            if (numberEntriesDto.Id != dataPointId) return BadRequest("Error with dataPointId");
            var checkDelete =
                await _numberEntriesRepository.DeleteEntryByIdAndTimeStampAsync(dataPointId, numberEntriesDto.TimeStamp);
            if (!checkDelete) return BadRequest("Datenpunkt konnte nicht gelöscht werden");
            return Ok(checkDelete);
        }
    }
}
