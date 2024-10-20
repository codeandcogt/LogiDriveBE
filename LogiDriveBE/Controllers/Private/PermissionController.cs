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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionBao _permissionBao;

        public PermissionController(IPermissionBao permissionBao)
        {
            _permissionBao = permissionBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Permission>>> CreatePermission([FromBody] CreatePermissionDto createPermissionDto)
        {
            try
            {
                var permission = new Permission
                {
                    Name = createPermissionDto.Name,
                    Description = createPermissionDto.Description,
                    Status = true,
                    CreationDate = DateTime.UtcNow
                };

            var response = await _permissionBao.CreatePermissionAsync(permission);
            return StatusCode(response.Code, response);

            }
            catch (Exception e)
            {
                return StatusCode(500, new OperationResponse<Permission>(500, e.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Permission>>> GetPermission(int id)
        {
            var response = await _permissionBao.GetPermissionByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Permission>>>> GetAllPermissions()
        {
            var response = await _permissionBao.GetAllPermissionsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Permission>>> UpdatePermission(int id, [FromBody] Permission permission)
        {
            if (id != permission.IdPermission)
            {
                return BadRequest(new OperationResponse<Permission>(400, "ID mismatch"));
            }
            var response = await _permissionBao.UpdatePermissionAsync(permission);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeletePermission(int id)
        {
            var response = await _permissionBao.DeletePermissionAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
