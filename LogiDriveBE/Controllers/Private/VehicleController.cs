using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleBao _vehicleBao;

        public VehicleController(IVehicleBao vehicleBao)
        {
            _vehicleBao = vehicleBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Vehicle>>> CreateVehicle([FromBody] CreateVehicleDto createVehicleDto)
        {
            var vehicle = new Vehicle
            {
                Brand = createVehicleDto.Brand,
                Plate = createVehicleDto.Plate,
                Tyoe = createVehicleDto.Tyoe,
                Year = createVehicleDto.Year,
                Mileage = createVehicleDto.Mileage,
                Capacity = createVehicleDto.Capacity,
                StatusVehicle = createVehicleDto.StatusVehicle,
                Status = true,
                CreationDate = DateTime.Now,

            };
            var response = await _vehicleBao.CreateVehicleAsync(vehicle);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Vehicle>>> GetVehicle(int id)
        {
            var response = await _vehicleBao.GetVehicleByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Vehicle>>>> GetAllVehicles()
        {
            var response = await _vehicleBao.GetAllVehiclesAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Vehicle>>> UpdateVehicle(int id, [FromBody] CreateVehicleDto createVehicleDto)
        {

            var existingVehicleReponse = await _vehicleBao.GetVehicleByIdAsync(id);
            if (existingVehicleReponse.Code != 200) 
            {
                return StatusCode(existingVehicleReponse.Code, existingVehicleReponse);
            }

            var existingVehicle = existingVehicleReponse.Data;

            existingVehicle.Brand = createVehicleDto.Brand;
            existingVehicle.Plate = createVehicleDto.Plate;
            existingVehicle.Tyoe = createVehicleDto.Tyoe;
            existingVehicle.Year = createVehicleDto.Year;
            existingVehicle.Mileage = createVehicleDto.Mileage;
            existingVehicle.Capacity = createVehicleDto.Capacity;
            existingVehicle.StatusVehicle = createVehicleDto.StatusVehicle;
            existingVehicle.Status = createVehicleDto.Status;

            var response = await _vehicleBao.UpdateVehicleAsync(existingVehicle);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteVehicle(int id)
        {
            var response = await _vehicleBao.DeleteVehicleAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteVehicleStatus(int id)
        {
            var response = await _vehicleBao.DeleteVehicleStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }

}
