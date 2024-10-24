using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LogTrackingController : ControllerBase
    {
        private readonly ILogTrackingBao _logTrackingBao;

        public LogTrackingController(ILogTrackingBao logTrackingBao)
        {
            _logTrackingBao = logTrackingBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogTracking>>> CreateLogTracking([FromBody] LogTrackingDto logTrackingDto)
        {
            var logTracking = new LogTracking
            {
                Latitude = logTrackingDto.Latitude,
                Longitude = logTrackingDto.Longitude
            };

            var response = await _logTrackingBao.CreateLogTrackingAsync(logTracking);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogTracking>>> GetLogTracking(int id)
        {
            var response = await _logTrackingBao.GetLogTrackingByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogTracking>>>> GetAllLogTrackings()
        {
            var response = await _logTrackingBao.GetAllLogTrackingsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<LogTracking>>> UpdateLogTracking(int id, [FromBody] LogTrackingDto logTrackingDto)
        {
            var logTracking = new LogTracking
            {
                IdTracking = id,
                Latitude = logTrackingDto.Latitude,
                Longitude = logTrackingDto.Longitude
            };

            var response = await _logTrackingBao.UpdateLogTrackingAsync(logTracking);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogTracking(int id)
        {
            var response = await _logTrackingBao.DeleteLogTrackingAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogTrackingStatus(int id)
        {
            var response = await _logTrackingBao.DeleteLogTrackingStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
