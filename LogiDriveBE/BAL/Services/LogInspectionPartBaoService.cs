using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Services
{
    public class LogInspectionPartBaoService : ILogInspectionPartBao
    {
        private readonly ILogInspectionPartDao _logInspectionPartDao;

        public LogInspectionPartBaoService(ILogInspectionPartDao logInspectionPartDao)
        {
            _logInspectionPartDao = logInspectionPartDao;
        }

        // Convert DTO to entity and call DAO method
        public async Task<OperationResponse<LogInspectionPartDto>> CreateLogInspectionPartAsync(LogInspectionPartDto logInspectionPartDto)
        {
            // Map DTO to entity
            var logInspectionPart = new LogInspectionPart
            {
                IdPartVehicle = logInspectionPartDto.IdPartVehicle,
                Comment = logInspectionPartDto.Comment,
                Status = logInspectionPartDto.Status,
                Image = logInspectionPartDto.Image
            };

            // Call DAO and get the result
            var result = await _logInspectionPartDao.CreateLogInspectionPartAsync(logInspectionPart);

            // Map entity back to DTO and return
            var dto = new LogInspectionPartDto
            {
                IdPartVehicle = result.Data.IdPartVehicle,
                Comment = result.Data.Comment,
                Status = result.Data.Status,
                Image = result.Data.Image
            };

            return new OperationResponse<LogInspectionPartDto>(result.Code, result.Message, dto);
        }

        public async Task<OperationResponse<LogInspectionPartDto>> GetLogInspectionPartByIdAsync(int id)
        {
            var result = await _logInspectionPartDao.GetLogInspectionPartByIdAsync(id);

            if (result.Code == 200 && result.Data != null)
            {
                var dto = new LogInspectionPartDto
                {
                    IdPartVehicle = result.Data.IdPartVehicle,
                    Comment = result.Data.Comment,
                    Status = result.Data.Status,
                    Image = result.Data.Image
                };

                return new OperationResponse<LogInspectionPartDto>(result.Code, result.Message, dto);
            }

            return new OperationResponse<LogInspectionPartDto>(result.Code, result.Message);
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionPartDto>>> GetAllLogInspectionPartsAsync()
        {
            var result = await _logInspectionPartDao.GetAllLogInspectionPartsAsync();

            if (result.Code == 200 && result.Data != null)
            {
                var dtoList = result.Data.Select(part => new LogInspectionPartDto
                {
                    IdPartVehicle = part.IdPartVehicle,
                    Comment = part.Comment,
                    Status = part.Status,
                    Image = part.Image
                }).ToList();

                return new OperationResponse<IEnumerable<LogInspectionPartDto>>(result.Code, result.Message, dtoList);
            }

            return new OperationResponse<IEnumerable<LogInspectionPartDto>>(result.Code, result.Message);
        }

        // Update method with mapping
        public async Task<OperationResponse<LogInspectionPartDto>> UpdateLogInspectionPartAsync(int id, LogInspectionPartDto logInspectionPartDto)
        {
            var logInspectionPart = new LogInspectionPart
            {
                IdPartVehicle = logInspectionPartDto.IdPartVehicle,
                Comment = logInspectionPartDto.Comment,
                Status = logInspectionPartDto.Status,
                Image = logInspectionPartDto.Image
            };

            var result = await _logInspectionPartDao.UpdateLogInspectionPartAsync(logInspectionPart);

            if (result.Code == 200 && result.Data != null)
            {
                var dto = new LogInspectionPartDto
                {
                    IdPartVehicle = result.Data.IdPartVehicle,
                    Comment = result.Data.Comment,
                    Status = result.Data.Status,
                    Image = result.Data.Image
                };

                return new OperationResponse<LogInspectionPartDto>(result.Code, result.Message, dto);
            }

            return new OperationResponse<LogInspectionPartDto>(result.Code, result.Message);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionPartAsync(int id)
        {
            return await _logInspectionPartDao.DeleteLogInspectionPartAsync(id);
        }
    }
}
