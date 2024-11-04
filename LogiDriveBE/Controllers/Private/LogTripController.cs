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
    public class LogTripController : ControllerBase
    {
        private readonly ILogTripBao _logTripBao;

        public LogTripController(ILogTripBao logTripBao)
        {
            _logTripBao = logTripBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogTrip>>> CreateLogTrip([FromBody] LogTripDto logTripDto)
        {
            var logTrip = new LogTrip
            {
                DateHour = logTripDto.DateHour,
                ActivityType = logTripDto.ActivityType,
                IdTracking = logTripDto.IdTracking,
                IdVehicleAssignment = logTripDto.IdVehicleAssignment,
                Status = true
            };

            var response = await _logTripBao.CreateLogTripWithActivityAsync(logTrip);
            return StatusCode(response.Code, response);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogTrip>>> GetLogTrip(int id)
        {
            var response = await _logTripBao.GetLogTripByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogTrip>>>> GetAllLogTrips()
        {
            var response = await _logTripBao.GetAllLogTripsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<LogTrip>>> UpdateLogTrip(int id, [FromBody] LogTripDto logTripDto)
        {
            var logTrip = new LogTrip
            {
                IdLogTrip = id,
                DateHour = logTripDto.DateHour,
                ActivityType = logTripDto.ActivityType,
                IdTracking = logTripDto.IdTracking,
                IdVehicleAssignment = logTripDto.IdVehicleAssignment,
                Status = logTripDto.Status
            };

            var response = await _logTripBao.UpdateLogTripAsync(logTrip);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogTrip(int id)
        {
            var response = await _logTripBao.DeleteLogTripAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogTripStatus(int id)
        {
            var response = await _logTripBao.DeleteLogTripStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
