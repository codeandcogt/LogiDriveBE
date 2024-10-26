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
    public class LogInspectionBaoService : ILogInspectionBao
    {
        private readonly ILogInspectionDao _logInspectionDao;
        private readonly ILogProcessDao _logProcessDao;
        private readonly IVehicleBao _vehicleBao;
        private readonly IMaintenancePartBao _maintenancePartBao;

        public LogInspectionBaoService(ILogInspectionDao logInspectionDao, ILogProcessDao logProcessDao, IVehicleBao vehicleBao, IMaintenancePartBao maintenancePartBao)
        {
            _logInspectionDao = logInspectionDao;
            _logProcessDao = logProcessDao;
            _vehicleBao = vehicleBao;
            _maintenancePartBao = maintenancePartBao;
        }


        public async Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id)
        {
            var inspectionResponse = await _logInspectionDao.GetLogInspectionByIdAsync(id);

            if (inspectionResponse.Code != 200)
            {
                return new OperationResponse<LogInspectionDto>(inspectionResponse.Code, inspectionResponse.Message);
            }

            // Mapear entidad a DTO
            var logInspectionDto = MapToLogInspectionDto(inspectionResponse.Data);
            return new OperationResponse<LogInspectionDto>(200, "Inspección obtenida exitosamente", logInspectionDto);
        }
        public async Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto)
        {
            try
            {
                // Mapear de DTO a entidad
                var logInspection = MapToLogInspection(logInspectionDto);
                var inspectionResponse = await _logInspectionDao.CreateLogInspectionAsync(logInspection);

                if (inspectionResponse.Code != 200)
                {
                    return new OperationResponse<LogInspectionDto>(inspectionResponse.Code, inspectionResponse.Message);
                }

                // Si LogProcess es null, lo creamos automáticamente
                if (logInspectionDto.LogProcess == null)
                {
                    var logProcess = new LogProcess
                    {
                        Action = "Creación de Inspección",
                        IdCollaborator = logInspectionDto.IdCollaborator,
                        IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                        IdLogInspection = inspectionResponse.Data.IdLogInspection
                    };

                    var processResponse = await _logProcessDao.CreateLogProcessAsync(MapToLogProcessDto(logProcess));

                    if (processResponse.Code != 200)
                    {
                        return new OperationResponse<LogInspectionDto>(processResponse.Code, processResponse.Message);
                    }

                    logInspectionDto.LogProcess = MapToLogProcessDto(logProcess);
                }

                // Mapear de entidad a DTO para la respuesta
                var createdLogInspectionDto = MapToLogInspectionDto(inspectionResponse.Data);

                return new OperationResponse<LogInspectionDto>(200, "LogInspection y LogProcess creados exitosamente", createdLogInspectionDto);
            }
            catch (System.Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500, $"Error creando la inspección: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync()
        {
            var inspectionsResponse = await _logInspectionDao.GetAllLogInspectionsAsync();

            if (inspectionsResponse.Code != 200)
            {
                return new OperationResponse<IEnumerable<LogInspectionDto>>(inspectionsResponse.Code, inspectionsResponse.Message);
            }

            // Mapear cada entidad a DTO
            var inspectionsDto = inspectionsResponse.Data.Select(MapToLogInspectionDto).ToList();

            return new OperationResponse<IEnumerable<LogInspectionDto>>(200, "Inspecciones obtenidas exitosamente", inspectionsDto);
        }

        public async Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto)
        {
            var logInspection = MapToLogInspection(logInspectionDto);
            logInspection.IdLogInspection = id;

            var updateResponse = await _logInspectionDao.UpdateLogInspectionAsync(id, logInspection);

            if (updateResponse.Code != 200)
            {
                return new OperationResponse<LogInspectionDto>(updateResponse.Code, updateResponse.Message);
            }

            var updatedLogInspectionDto = MapToLogInspectionDto(updateResponse.Data);
            return new OperationResponse<LogInspectionDto>(200, "Inspección actualizada exitosamente", updatedLogInspectionDto);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            return await _logInspectionDao.DeleteLogInspectionAsync(id);
        }

        // Mapeo de LogInspection entidad a DTO
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
                Status = entity.Status,
                CreationDate = entity.CreationDate,
                Image = entity.Image,
                PartsInspected = entity.LogInspectionParts?.Select(p => new LogInspectionPartDto
                {
                    IdPartVehicle = p.IdPartVehicle,
                    Comment = p.Comment,
                    Status = p.Status,
                    Image = p.Image,
                    DateInspection = p.DateInspection
                }).ToList(),
                // Agregar LogProcess si es necesario en la respuesta
                LogProcess = new LogProcessDto()
            };
        }

        // Mapeo de DTO a entidad LogInspection
        // En el método MapToLogInspection
        private LogInspection MapToLogInspection(LogInspectionDto dto)
        {
            return new LogInspection
            {
                IdCollaborator = dto.IdCollaborator,
                IdVehicleAssignment = dto.IdVehicleAssignment,
                Comment = dto.Comment,
                Odometer = dto.Odometer,
                Fuel = dto.Fuel,
                TypeInspection = dto.TypeInspection,
                Status = dto.Status,
                CreationDate = dto.CreationDate,
                Image = dto.Image,
                LogInspectionParts = dto.PartsInspected?.Select(p => new LogInspectionPart
                {
                    IdPartVehicle = p.IdPartVehicle,
                    Comment = p.Comment,
                    Status = p.Status,
                    Image = p.Image,
                    DateInspection = p.DateInspection
                }).ToList()
            };
        }


        // Mapeo de LogProcess a LogProcessDto
        private LogProcessDto MapToLogProcessDto(LogProcess logProcess)
        {
            return new LogProcessDto
            {
                IdLogReservation = logProcess.IdLogReservation,
                Action = logProcess.Action,
                IdCollaborator = logProcess.IdCollaborator,
                IdVehicleAssignment = logProcess.IdVehicleAssignment,
                IdLogInspection = logProcess.IdLogInspection
            };
        }

        public async Task<OperationResponse<LogInspectionDto>> ProcessInspectionAsync(LogInspectionDto logInspectionDto)
        {
            try
            {
                // Verificar si la inspección es de recepción y que haya una de entrega previa
                if (logInspectionDto.TypeInspection == "Recepción")
                {
                    var deliveryInspection = await _logInspectionDao.GetLogInspectionByVehicleAssignmentAsync(logInspectionDto.IdVehicleAssignment, "Entrega");
                    if (deliveryInspection == null || deliveryInspection.Code != 200)
                    {
                        return new OperationResponse<LogInspectionDto>(400, "No se puede crear una inspección de recepción sin una de entrega previa.");
                    }
                }

                // Crear la inspección y las partes relacionadas
                var inspectionResponse = await CreateLogInspectionAsync(logInspectionDto);
                if (inspectionResponse.Code != 200)
                {
                    return inspectionResponse;
                }

                // Manejo de partes defectuosas
                foreach (var part in logInspectionDto.PartsInspected)
                {
                    if (part.Status == false) // Asumimos que 'false' indica parte defectuosa
                    {
                        // Cambiar el estado de la parte y enviarla a mantenimiento
                        await _maintenancePartBao.SendPartToMaintenanceAsync(part.IdPartVehicle);

                        // Cambiar el estado del vehículo a "En servicio"
                        var vehicleResponse = await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                        if (vehicleResponse.Code != 200)
                        {
                            return new OperationResponse<LogInspectionDto>(500, $"Error actualizando el estado del vehículo: {vehicleResponse.Message}");
                        }
                    }
                }

                // Lógica del odómetro
                if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
                {
                    var vehicleResponse = await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                    if (vehicleResponse.Code != 200)
                    {
                        return new OperationResponse<LogInspectionDto>(500, $"Error actualizando el estado del vehículo: {vehicleResponse.Message}");
                    }
                }

                // Crear el LogProcess correspondiente
                var logProcessDto = new LogProcessDto
                {
                    Action = "Inspección de vehículo",
                    IdCollaborator = logInspectionDto.IdCollaborator,
                    IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                    IdLogInspection = inspectionResponse.Data.IdLogInspection
                };
                await _logProcessDao.CreateLogProcessAsync(logProcessDto);

                return new OperationResponse<LogInspectionDto>(200, "Inspección procesada exitosamente", inspectionResponse.Data);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500, $"Error procesando la inspección: {ex.Message}");
            }
        }

    }
}
