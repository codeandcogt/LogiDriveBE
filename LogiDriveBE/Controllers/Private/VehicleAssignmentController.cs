using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleAssignmentController : ControllerBase
    {
        private readonly IVehicleAssignmentBao _vehicleAssignmentBao;

        public VehicleAssignmentController(IVehicleAssignmentBao vehicleAssignmentBao)
        {
            _vehicleAssignmentBao = vehicleAssignmentBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<VehicleAssignmentDto>>> CreateVehicleAssignment([FromBody] VehicleAssignmentDto vehicleAssignmentDto)
        {
            var response = await _vehicleAssignmentBao.CreateVehicleAssignmentAsync(vehicleAssignmentDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<VehicleAssignmentDto>>> GetVehicleAssignmentById(int id)
        {
            var response = await _vehicleAssignmentBao.GetVehicleAssignmentByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<VehicleAssignmentDto>>>> GetAllVehicleAssignments()
        {
            var response = await _vehicleAssignmentBao.GetAllVehicleAssignmentsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse<VehicleAssignmentDto>>> UpdateVehicleAssignment([FromBody] VehicleAssignmentDto vehicleAssignmentDto)
        {
            var response = await _vehicleAssignmentBao.UpdateVehicleAssignmentAsync(vehicleAssignmentDto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteVehicleAssignment(int id)
        {
            var response = await _vehicleAssignmentBao.DeleteVehicleAssignmentAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> UpdateStatusVehicleAssignment(int id)
        {
            var response = await _vehicleAssignmentBao.DeleteVehicleAssignmentStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
