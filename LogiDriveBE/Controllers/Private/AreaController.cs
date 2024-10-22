using Azure.Core;
using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly IAreaBao _areaBao;

        public AreaController(IAreaBao areaBao)
        {
            _areaBao = areaBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Area>>> CreateArea([FromBody] CreateAreaDto createAreaDto)
        {
            var area = new Area
            {
                Name = createAreaDto.Name,
                Description = createAreaDto.Description,
                Status = true,
                CreationDate = DateTime.UtcNow
            };

            var response = await _areaBao.CreateAreaAsync(area);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Area>>> GetArea(int id)
        {
            var response = await _areaBao.GetAreaByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Area>>>> GetAllAreas()
        {
            var response = await _areaBao.GetAllAreasAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Area>>> UpdateArea(int id, [FromBody] UpdateAreaDto updateAreaDto)
        {
            var existingAreaResponse = await _areaBao.GetAreaByIdAsync(id);
            if (existingAreaResponse.Code != 200)
            {
                return StatusCode(existingAreaResponse.Code, existingAreaResponse);
            }

            var existingArea = existingAreaResponse.Data;

            // Mapear los valores de la solicitud a la entidad existente
            existingArea.Name = updateAreaDto.Name;
            existingArea.Description = updateAreaDto.Description;
            existingArea.Status = updateAreaDto.Status; 

            // Llamar al servicio para actualizar el área
            var response = await _areaBao.UpdateAreaAsync(existingArea);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteArea(int id)
        {
            var response = await _areaBao.DeleteAreaAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteAreaStatus(int id)
        {
            var response = await _areaBao.DeleteAreaStatusAsync(id);
            return StatusCode(response.Code, response);
        }

    }
}
