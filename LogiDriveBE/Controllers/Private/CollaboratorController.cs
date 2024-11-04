using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBao _collaboratorBao;

        public CollaboratorController(ICollaboratorBao collaboratorBao)
        {
            _collaboratorBao = collaboratorBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Collaborator>>> CreateCollaborator([FromBody] Collaborator collaborator)
        {
            var response = await _collaboratorBao.CreateCollaboratorAsync(collaborator);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Collaborator>>> GetCollaborator(int id)
        {
            var response = await _collaboratorBao.GetCollaboratorByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Collaborator>>>> GetAllCollaborators()
        {
            var response = await _collaboratorBao.GetAllCollaboratorsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Collaborator>>> UpdateCollaborator(int id, [FromBody] Collaborator collaborator)
        {
            if (id != collaborator.IdCollaborator)
            {
                return BadRequest(new OperationResponse<Collaborator>(400, "ID mismatch"));
            }

            var response = await _collaboratorBao.UpdateCollaboratorAsync(collaborator);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteCollaborator(int id)
        {
            var response = await _collaboratorBao.DeleteCollaboratorAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
