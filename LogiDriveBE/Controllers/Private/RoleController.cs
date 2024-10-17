using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBao _roleBao;

        public RoleController(IRoleBao roleBao)
        {
            _roleBao = roleBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Role>>> CreateRole([FromBody] Role role)
        {
            var response = await _roleBao.CreateRoleAsync(role);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Role>>> GetRole(int id)
        {
            var response = await _roleBao.GetRoleByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Role>>>> GetAllRoles()
        {
            var response = await _roleBao.GetAllRolesAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Role>>> UpdateRole(int id, [FromBody] Role role)
        {
            if (id != role.IdRole)
            {
                return BadRequest(new OperationResponse<Role>(400, "ID mismatch"));
            }
            var response = await _roleBao.UpdateRoleAsync(role);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteRole(int id)
        {
            var response = await _roleBao.DeleteRoleAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpPost("{id}/permissions")]
        public async Task<ActionResult<OperationResponse<Role>>> AssignPermissionsToRole(int id, [FromBody] List<int> permissionIds)
        {
            var response = await _roleBao.AssignPermissionsToRoleAsync(id, permissionIds);
            return StatusCode(response.Code, response);
        }
    }
}
