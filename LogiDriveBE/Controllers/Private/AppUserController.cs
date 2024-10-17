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
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserBao _appUserBao;

        public AppUserController(IAppUserBao appUserBao)
        {
            _appUserBao = appUserBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<AppUser>>> CreateAppUserWithCollaborator([FromBody] AppUserCollaboratorDto dto)
        {
            var appUser = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Status = true
            };

            var collaborator = new Collaborator
            {
                Name = dto.CollaboratorName,
                LastName = dto.CollaboratorLastName,
                Position = dto.Position,
                Phone = dto.Phone,
                Email = dto.Email,
                Status = true,
                CreationDate = DateTime.UtcNow,
                IdArea = dto.IdArea
            };

            var response = await _appUserBao.CreateAppUserWithCollaboratorAsync(appUser, collaborator, dto.IdRole);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<AppUser>>> GetAppUser(int id)
        {
            var response = await _appUserBao.GetAppUserByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<AppUser>>>> GetAllAppUsers()
        {
            var response = await _appUserBao.GetAllAppUsersAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<AppUser>>> UpdateAppUser(int id, [FromBody] AppUser appUser)
        {
            if (id != appUser.IdAppUser)
            {
                return BadRequest(new OperationResponse<AppUser>(400, "ID mismatch"));
            }

            var response = await _appUserBao.UpdateAppUserAsync(appUser);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteAppUser(int id)
        {
            var response = await _appUserBao.DeleteAppUserAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}
