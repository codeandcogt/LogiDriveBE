using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInspectionController : ControllerBase
    {
        private readonly ILogInspectionBao _logInspectionBao;

        public LogInspectionController(ILogInspectionBao logInspectionBao)
        {
            _logInspectionBao = logInspectionBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> CreateLogInspection([FromBody] LogInspectionDto logInspectionDto)
        {
            var response = await _logInspectionBao.CreateLogInspectionAsync(logInspectionDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> GetLogInspection(int id)
        {
            var response = await _logInspectionBao.GetLogInspectionByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogInspectionDto>>>> GetAllLogInspections()
        {
            var response = await _logInspectionBao.GetAllLogInspectionsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> UpdateLogInspection(int id, [FromBody] LogInspectionDto logInspectionDto)
        {
            var response = await _logInspectionBao.UpdateLogInspectionAsync(id, logInspectionDto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogInspection(int id)
        {
            var response = await _logInspectionBao.DeleteLogInspectionAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
