using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogInspectionController : ControllerBase
    {
        private readonly ILogInspectionBao _logInspectionBao;
        private readonly IVehicleBao _vehicleBao;

        public LogInspectionController(ILogInspectionBao logInspectionBao, IVehicleBao vehicleBao)
        {
            _logInspectionBao = logInspectionBao;
            _vehicleBao = vehicleBao;
        }

        //[HttpPost]
        //public async Task<ActionResult<OperationResponse<LogInspectionDto>>> CreateLogInspection([FromBody] LogInspectionDto logInspectionDto)
        //{
        //    var response = await _logInspectionBao.CreateLogInspectionAsync(logInspectionDto);

        //    if (response.Code == 200)
        //    {
        //        // Lógica del odómetro: si el kilometraje supera los 25,000 km, cambiar el estado del vehículo a 'En servicio'
        //        if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
        //        {
        //            var vehicleResponse = await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
        //            if (vehicleResponse.Code != 200)
        //            {
        //                return StatusCode(500, $"Error updating vehicle status: {vehicleResponse.Message}");
        //            }
        //        }
        //    }

        //    return StatusCode(response.Code, response);
        //}
        [HttpPost]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> CreateLogInspection([FromBody] LogInspectionDto logInspectionDto)
        {
            if (logInspectionDto == null)
                return BadRequest("Los datos de la inspección son requeridos");

            var response = await _logInspectionBao.CreateLogInspectionAsync(logInspectionDto);
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> UpdateLogInspection(
            int id,
            [FromForm] LogInspectionDto logInspectionDto)
        {
            var response = await _logInspectionBao.UpdateLogInspectionAsync(id, logInspectionDto);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteLogInspection(int id)
        {
            var response = await _logInspectionBao.DeleteLogInspectionAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost("process")]
        public async Task<ActionResult<OperationResponse<LogInspectionDto>>> ProcessInspection([FromBody] LogInspectionDto logInspectionDto)
        {
            var response = await _logInspectionBao.ProcessInspectionAsync(logInspectionDto);
            return StatusCode(response.Code, response);
        }

    }
}
