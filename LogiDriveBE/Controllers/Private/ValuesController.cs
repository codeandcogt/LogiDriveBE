﻿using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TownController : ControllerBase
    {
        private readonly ITownBao _townBao;

        public TownController(ITownBao townBao)
        {
            _townBao = townBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<TownDto>>> CreateTown([FromBody] TownDto townDto)
        {
            var response = await _townBao.CreateTownAsync(townDto);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<TownDto>>> GetTownById(int id)
        {
            var response = await _townBao.GetTownByIdAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<TownDto>>>> GetAllTowns()
        {
            var response = await _townBao.GetAllTownsAsync();
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<TownDto>>> UpdateTown(int id, [FromBody] TownDto townDto)
        {
            // Verificar que el ID proporcionado coincide con el ID del DTO
            if (id != townDto.IdTown)
            {
                return BadRequest(new OperationResponse<TownDto>(400, "ID mismatch between URL and payload"));
            }

            var response = await _townBao.UpdateTownAsync(townDto);
            return StatusCode(response.Code, response);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteTown(int id)
        {
            var response = await _townBao.DeleteTownAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> UpdateStatusTown(int id)
        {
            var response = await _townBao.DeleteTownStatusAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet("byDepartment/{departmentId}")]
        public async Task<ActionResult<OperationResponse<IEnumerable<TownDto>>>> GetTownsByDepartmentId(int departmentId)
        {
            var response = await _townBao.GetTownsByDepartmentIdAsync(departmentId);
            return StatusCode(response.Code, response);
        }

    }
}
