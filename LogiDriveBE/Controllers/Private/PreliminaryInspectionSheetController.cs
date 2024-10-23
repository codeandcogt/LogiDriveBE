using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class PreliminaryInspectionSheetController : ControllerBase
    {
        private readonly IPreliminaryInspectionSheetBao _bao;

        public PreliminaryInspectionSheetController(IPreliminaryInspectionSheetBao bao)
        {
            _bao = bao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<PreliminaryInspectionSheetDto>>> Create([FromBody] PreliminaryInspectionSheetDto dto)
        {
            var response = await _bao.CreatePreliminaryInspectionSheetAsync(dto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<PreliminaryInspectionSheetDto>>> GetById(int id)
        {
            var response = await _bao.GetPreliminaryInspectionSheetByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<PreliminaryInspectionSheetDto>>>> GetAll()
        {
            var response = await _bao.GetAllPreliminaryInspectionSheetsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut]
        public async Task<ActionResult<OperationResponse<PreliminaryInspectionSheetDto>>> Update([FromBody] PreliminaryInspectionSheetDto dto)
        {
            var response = await _bao.UpdatePreliminaryInspectionSheetAsync(dto);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> Delete(int id)
        {
            var response = await _bao.DeletePreliminaryInspectionSheetAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
