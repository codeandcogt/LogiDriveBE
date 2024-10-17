using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
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
        public async Task<ActionResult<OperationResponse<Area>>> CreateArea([FromBody] Area area)
        {
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
        public async Task<ActionResult<OperationResponse<Area>>> UpdateArea(int id, [FromBody] Area area)
        {
            if (id != area.IdArea)
            {
                return BadRequest(new OperationResponse<Area>(400, "ID mismatch"));
            }

            var response = await _areaBao.UpdateAreaAsync(area);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteArea(int id)
        {
            var response = await _areaBao.DeleteAreaAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
