using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogReservationController : ControllerBase
    {
        private readonly ILogReservationBao _logReservationBao;

        public LogReservationController(ILogReservationBao logReservationBao)
        {
            _logReservationBao = logReservationBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogReservationDto>>> CreateLogReservation([FromBody] LogReservationDto logReservationDto)
        {
            var response = await _logReservationBao.CreateLogReservationAsync(logReservationDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogReservationDto>>> GetLogReservationById(int id)
        {
            var response = await _logReservationBao.GetLogReservationByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogReservationDto>>>> GetAllLogReservations()
        {
            var response = await _logReservationBao.GetAllLogReservationsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse<LogReservationDto>>> UpdateLogReservation([FromBody] LogReservationDto logReservationDto)
        {
            var response = await _logReservationBao.UpdateLogReservationAsync(logReservationDto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogReservation(int id)
        {
            var response = await _logReservationBao.DeleteLogReservationAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<OperationResponse<bool>>> UpdateStatusReservation(int id, [FromBody] UpdateStatusReservationDto updateStatusReservationDto)
        {
            var response = await _logReservationBao.UpdateStatusReservationAsync(id, updateStatusReservationDto);
            return StatusCode(response.Code, response);
        }
    }
}
