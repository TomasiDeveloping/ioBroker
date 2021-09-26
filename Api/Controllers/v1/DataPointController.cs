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
    public class DataPointController : ControllerBase
    {
        private readonly IDataPointRepository _dataPointRepository;

        public DataPointController(IDataPointRepository dataPointRepository)
        {
            _dataPointRepository = dataPointRepository;
        }

        [HttpGet("{dataPointId:int}")]
        public async Task<ActionResult<DataPointDto>> GetDataPointById(int dataPointId)
        {
            var dataPoint = await _dataPointRepository.GetDataPointByIdAsync(dataPointId);
            if (dataPoint == null) return NoContent();
            return Ok(dataPoint);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<DataPointDto>>> GetDataPointsByName(string name)
        {
            var dataPoints = await _dataPointRepository.GetDataPointsByNameAsync(name);
            if (dataPoints.Count <= 0) return NoContent();
            return Ok(dataPoints);
        }

        [HttpGet]
        public async Task<ActionResult<List<DataPointDto>>> GetDataPoints()
        {
            var dataPoints = await _dataPointRepository.GetDataPointsAsync();
            if (dataPoints.Count <= 0) return NoContent();
            return Ok(dataPoints);  
        }
    }
}
