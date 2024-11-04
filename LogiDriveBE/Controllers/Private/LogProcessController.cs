using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LogProcessController : ControllerBase
    {
        private readonly ILogProcessBao _logProcessBao;

        public LogProcessController(ILogProcessBao logProcessBao)
        {
            _logProcessBao = logProcessBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogProcessDto>>> CreateLogProcess([FromBody] LogProcessDto logProcessDto)
        {
            var response = await _logProcessBao.CreateLogProcessAsync(logProcessDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<LogProcessDto>>> GetLogProcessById(int id)
        {
            var response = await _logProcessBao.GetLogProcessByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<LogProcessDto>>>> GetAllLogProcesses()
        {
            var response = await _logProcessBao.GetAllLogProcessesAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse<LogProcessDto>>> UpdateLogProcess([FromBody] LogProcessDto logProcessDto)
        {
            var response = await _logProcessBao.UpdateLogProcessAsync(logProcessDto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogProcess(int id)
        {
            var response = await _logProcessBao.DeleteLogProcessAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
