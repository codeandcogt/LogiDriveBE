using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MaintenancePartController : ControllerBase
    {
        private readonly IMaintenancePartBao _maintenancePartBao;

        public MaintenancePartController(IMaintenancePartBao maintenancePartBao)
        {
            _maintenancePartBao = maintenancePartBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> CreateMaintenancePart([FromBody] MaintenancePartDto maintenancePartDto)
        {
            var maintenancePart = new MaintenancePart
            {
                Comment = maintenancePartDto.Comment,
                DateMaintenancePart = maintenancePartDto.DateMaintenancePart,
                IdPartVehicle = maintenancePartDto.IdPartVehicle,
                Status = maintenancePartDto.Status
            };
            var response = await _maintenancePartBao.CreateMaintenancePartAsync(maintenancePart);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> GetMaintenancePart(int id)
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
        public async Task<ActionResult<OperationResponse<MaintenancePart>>> UpdateMaintenancePart(int id, [FromBody] MaintenancePartDto updateMaintenancePartDto)
        {
            var existingMaintenancePartResponse = await _maintenancePartBao.GetMaintenancePartAsync(id);
            if (existingMaintenancePartResponse.Code != 200)    
            {
                return StatusCode(existingMaintenancePartResponse.Code, existingMaintenancePartResponse);
            }
            var existingMaintenancePart = existingMaintenancePartResponse.Data;
            existingMaintenancePart.Comment = updateMaintenancePartDto.Comment;
            existingMaintenancePart.DateMaintenancePart = updateMaintenancePartDto.DateMaintenancePart;
            existingMaintenancePart.IdPartVehicle = updateMaintenancePartDto.IdPartVehicle;
            existingMaintenancePart.Status = updateMaintenancePartDto.Status;
            var response = await _maintenancePartBao.UpdateMaintenancePartAsync(existingMaintenancePart);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteMaintenancePart(int id)
        {
            var response = await _maintenancePartBao.DeleteMaintenancePartAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteMaintenancePartStatus(int id)
        {
            var response = await _maintenancePartBao.DeleteMaintenancePartStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}