using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;

namespace LogiDriveBE.Controllers.Private

{
    [ApiController]
    [Route("api/[controller]")]
    public class PartVehicleController : ControllerBase
    {
        private readonly IPartVehicleBao _partVehicleBao;

        public PartVehicleController(IPartVehicleBao partVehicleBao)
        {
            _partVehicleBao = partVehicleBao;
        }


        [HttpPost]
        public async Task<ActionResult<OperationResponse<PartVehicle>>> PartVehicle([FromBody] PartVehicleDto partVehicleDto)
        {
            var partVehicle = new PartVehicle
            {
                
                Name = partVehicleDto.Name,
                Description = partVehicleDto.Description,
                StatusPart = partVehicleDto.StatusPart,
                IdVehicle = partVehicleDto.IdVehicle,
                Status = true,


            };
            var response = await _partVehicleBao.CreatePartVehicleAsync(partVehicle);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<PartVehicle>>> GetVehicle(int id)
        {
            var response = await _partVehicleBao.GetPartVehicleByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<PartVehicle>>>> GetAllPartVehicles()
        {
            var response = await _partVehicleBao.GetAllPartVehiclesAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<PartVehicle>>> UpdatePartVehicle(int id, [FromBody] PartVehicleDto partVehicleDto)
        {

            var existingPartVehicleReponse = await _partVehicleBao.GetPartVehicleByIdAsync(id);
            if (existingPartVehicleReponse.Code != 200)
            {
                return StatusCode(existingPartVehicleReponse.Code, existingPartVehicleReponse);
            }

            var existingPartVehicle = existingPartVehicleReponse.Data;

            existingPartVehicle.Name = partVehicleDto.Name;
            existingPartVehicle.Description = partVehicleDto.Description;
            existingPartVehicle.StatusPart = partVehicleDto.StatusPart;
            existingPartVehicle.IdVehicle = partVehicleDto.IdVehicle;
            existingPartVehicle.Status = partVehicleDto.Status;

            var response = await _partVehicleBao.UpdatePartVehicleAsync(existingPartVehicle);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeletePartVehicle(int id)
        {
            var response = await _partVehicleBao.DeletePartVehicleAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeletePartVehicleStatus(int id)
        {
            var response = await _partVehicleBao.DeletePartVehicleStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}

