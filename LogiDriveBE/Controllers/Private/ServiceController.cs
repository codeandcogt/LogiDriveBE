using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceBao _serviceBao;

        public ServiceController(IServiceBao serviceBao)
        {
            _serviceBao = serviceBao;
        }

        [HttpPost]
        public async Task<ActionResult<OperationResponse<Service>>> CreateService([FromBody] ServiceDto serviceDto)
        {
            var service = new Service
            {
                IdVehicle = serviceDto.IdVehicle,
                Comment = serviceDto.Comment,
                Maintenance = serviceDto.Maintenance,
                NextServie = serviceDto.NextServie,
                IdTypeService = serviceDto.IdTypeService,
                Status = serviceDto.Status
            };

            var response = await _serviceBao.CreateServiceAsync(service);
            return StatusCode(response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationResponse<Service>>> GetService(int id)
        {
            var response = await _serviceBao.GetServiceAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpGet]
        public async Task<ActionResult<OperationResponse<IEnumerable<Service>>>> GetAllServices()
        {
            var response = await _serviceBao.GetAllServicesAsync();
            return StatusCode(response.Code, response);
        }

        [HttpGet("vehicle/{idVehicle}")]
        public async Task<ActionResult<OperationResponse<IEnumerable<Service>>>> GetServicesByVehicle(int idVehicle)
        {
            var response = await _serviceBao.GetServicesByVehicleAsync(idVehicle);
            return StatusCode(response.Code, response);
        }

        [HttpGet("type/{idTypeService}")]
        public async Task<ActionResult<OperationResponse<IEnumerable<Service>>>> GetServicesByType(int idTypeService)
        {
            var response = await _serviceBao.GetServicesByTypeAsync(idTypeService);
            return StatusCode(response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OperationResponse<Service>>> UpdateService(int id, [FromBody] ServiceDto updateserviceDto)
        {
            var existingServiceResponse = await _serviceBao.GetServiceAsync(id);
            if (existingServiceResponse.Code != 200)
            {
                return StatusCode(existingServiceResponse.Code, existingServiceResponse);
            }

            var existingService = existingServiceResponse.Data;

            existingService.IdVehicle = updateserviceDto.IdVehicle;
            existingService.Comment = updateserviceDto.Comment;
            existingService.Maintenance = updateserviceDto.Maintenance;
            existingService.NextServie = updateserviceDto.NextServie;
            existingService.IdTypeService = updateserviceDto.IdTypeService;
            existingService.Status = updateserviceDto.Status;

            var response = await _serviceBao.UpdateServiceAsync(existingService);
            return StatusCode(response.Code, response);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteService(int id)
        {
            var response = await _serviceBao.DeleteServiceAsync(id);
            return StatusCode(response.Code, response);
        }

        [HttpDelete("Status/{id}")]
        public async Task<ActionResult<OperationResponse<bool>>> DeleteServiceStatus(int id)
        {
            var response = await _serviceBao.DeleteServiceStatusAsync(id);
            return StatusCode(response.Code, response);
        }
    }
}