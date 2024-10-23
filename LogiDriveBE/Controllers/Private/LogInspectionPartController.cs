using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInspectionPartController : ControllerBase
    {
        private readonly ILogInspectionPartBao _logInspectionPartBao;

        public LogInspectionPartController(ILogInspectionPartBao logInspectionPartBao)
        {
            _logInspectionPartBao = logInspectionPartBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogInspectionPartDto>>> CreateLogInspectionPart([FromBody] LogInspectionPartDto logInspectionPartDto)
        {
            var response = await _logInspectionPartBao.CreateLogInspectionPartAsync(logInspectionPartDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogInspectionPartDto>>> GetLogInspectionPartById(int id)
        {
            var response = await _logInspectionPartBao.GetLogInspectionPartByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogInspectionPartDto>>>> GetAllLogInspectionParts()
        {
            var response = await _logInspectionPartBao.GetAllLogInspectionPartsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<LogInspectionPartDto>>> UpdateLogInspectionPart(int id, [FromBody] LogInspectionPartDto logInspectionPartDto)
        {
            var response = await _logInspectionPartBao.UpdateLogInspectionPartAsync(id, logInspectionPartDto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogInspectionPart(int id)
        {
            var response = await _logInspectionPartBao.DeleteLogInspectionPartAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
