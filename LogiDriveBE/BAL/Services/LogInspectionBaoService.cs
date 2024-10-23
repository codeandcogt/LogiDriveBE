using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.Models; // Agregar la referencia a los modelos
using LogiDriveBE.UTILS;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LogiDriveBE.BAL.Bao;

namespace LogiDriveBE.BAL.Services
{
    public class LogInspectionBaoService : ILogInspectionBao
    {
        private readonly ILogInspectionDao _logInspectionDao;
        private readonly IVehicleBao _vehicleBao;

        public LogInspectionBaoService(ILogInspectionDao logInspectionDao, IVehicleBao vehicleBao)
        {
            _logInspectionDao = logInspectionDao;
            _vehicleBao = vehicleBao;
        }

        // Mapeo de LogInspectionDto a LogInspection
        private LogInspection MapToLogInspection(LogInspectionDto dto)
        {
            return new LogInspection
            {
                IdLogInspection = dto.IdLogInspection,
                IdCollaborator = dto.IdCollaborator,
                IdVehicleAssignment = dto.IdVehicleAssignment,
                Comment = dto.Comment,
                Odometer = dto.Odometer,
                Fuel = dto.Fuel,
                TypeInspection = dto.TypeInspection,
                Image = dto.Image,
                Status = dto.Status,
                CreationDate = dto.CreationDate,
                LogInspectionParts = dto.PartsInspected.Select(part => new LogInspectionPart
                {
                    IdLogInspectionPart = part.IdLogInspectionPart,
                    IdPartVehicle = part.IdPartVehicle,
                    Comment = part.Comment,
                    Status = part.Status,
                    DateInspection = part.DateInspection,
                    Image = part.Image
                }).ToList()
            };
        }

        // Mapeo de LogInspection a LogInspectionDto
        private LogInspectionDto MapToLogInspectionDto(LogInspection entity)
        {
            return new LogInspectionDto
            {
                IdLogInspection = entity.IdLogInspection,
                IdCollaborator = entity.IdCollaborator,
                IdVehicleAssignment = entity.IdVehicleAssignment ?? 0,
                Comment = entity.Comment,
                Odometer = entity.Odometer,
                Fuel = entity.Fuel,
                TypeInspection = entity.TypeInspection,
                Image = entity.Image ?? string.Empty,
                Status = entity.Status,
                CreationDate = entity.CreationDate,
                PartsInspected = entity.LogInspectionParts.Select(part => new LogInspectionPartDto
                {
                    IdLogInspectionPart = part.IdLogInspectionPart,
                    IdPartVehicle = part.IdPartVehicle,
                    Comment = part.Comment,
                    Status = part.Status,
                    DateInspection = part.DateInspection,
                    Image = part.Image ?? string.Empty
                }).ToList()
            };
        }

        public async Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto)
        {
            var logInspection = MapToLogInspection(logInspectionDto); // Convertir DTO a entidad

            var response = await _logInspectionDao.CreateLogInspectionAsync(logInspection);

            if (response.Code == 200)
            {
                // Lógica del odómetro: si el kilometraje supera los 25,000 km, cambiar el estado del vehículo a 'En servicio'
                if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
                {
                    var vehicleResponse = await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                    if (vehicleResponse.Code != 200)
                    {
                        return new OperationResponse<LogInspectionDto>(500, $"Error updating vehicle status: {vehicleResponse.Message}");
                    }
                }
            }

            return new OperationResponse<LogInspectionDto>(response.Code, response.Message, logInspectionDto);
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync()
        {
            var response = await _logInspectionDao.GetAllLogInspectionsAsync();

            if (response.Code != 200)
            {
                return new OperationResponse<IEnumerable<LogInspectionDto>>(response.Code, response.Message);
            }

            // Mapeamos las entidades LogInspection a DTO
            var inspectionDtos = response.Data.Select(MapToLogInspectionDto).ToList();

            return new OperationResponse<IEnumerable<LogInspectionDto>>(200, "Inspections retrieved successfully", inspectionDtos);
        }

        public async Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id)
        {
            var response = await _logInspectionDao.GetLogInspectionByIdAsync(id);

            if (response.Code != 200)
            {
                return new OperationResponse<LogInspectionDto>(response.Code, response.Message);
            }

            var inspectionDto = MapToLogInspectionDto(response.Data); // Convertir entidad a DTO

            return new OperationResponse<LogInspectionDto>(200, "Inspection retrieved successfully", inspectionDto);
        }

        public async Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto)
        {
            var logInspection = MapToLogInspection(logInspectionDto); // Convertir DTO a entidad

            var response = await _logInspectionDao.UpdateLogInspectionAsync(id, logInspection);

            if (response.Code == 200)
            {
                // Lógica del odómetro en la actualización
                if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
                {
                    var vehicleResponse = await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                    if (vehicleResponse.Code != 200)
                    {
                        return new OperationResponse<LogInspectionDto>(500, $"Error updating vehicle status: {vehicleResponse.Message}");
                    }
                }
            }

            return new OperationResponse<LogInspectionDto>(response.Code, response.Message, logInspectionDto);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            return await _logInspectionDao.DeleteLogInspectionAsync(id);
        }
    }
}
