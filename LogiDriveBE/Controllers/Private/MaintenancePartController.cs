using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaintenancePartController : ControllerBase
    {
        private readonly IMaintenancePartBao _maintenancePartBao;

        public MaintenancePartController(IMaintenancePartBao maintenancePartBao)
        {
            _maintenancePartBao = maintenancePartBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> CreateMaintenancePart([FromBody] MaintenancePart maintenancePart)
        {
            var response = await _maintenancePartBao.CreateMaintenancePartAsync(maintenancePart);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> GetMaintenancePart(string id)
        {
            var response = await _maintenancePartBao.GetMaintenancePartAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<MaintenancePart>>>> GetAllMaintenanceParts()
        {
            var response = await _maintenancePartBao.GetAllMaintenancePartAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> UpdateMaintenancePart(int id, [FromBody] MaintenancePart maintenancePart)
        {
            if (id != maintenancePart.IdMaintenancePart)
            {
                return BadRequest(new OperationResponse<MaintenancePart>(400, "ID mismatch"));
            }
            var response = await _maintenancePartBao.UpdateMaintenancePartAsync(maintenancePart);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteMaintenancePart(int id)
        {
            var response = await _maintenancePartBao.DeleteMaintenancePartAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}