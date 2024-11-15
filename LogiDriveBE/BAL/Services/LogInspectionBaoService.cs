﻿using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.SAL.Sao;
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
        private readonly IS3Service _s3Service;

        public LogInspectionBaoService(ILogInspectionDao logInspectionDao, ILogProcessDao logProcessDao, IVehicleBao vehicleBao, IMaintenancePartBao maintenancePartBao, IS3Service s3Service)
        {
            _logInspectionDao = logInspectionDao;
            _logProcessDao = logProcessDao;
            _vehicleBao = vehicleBao;
            _maintenancePartBao = maintenancePartBao;
            _s3Service = s3Service;
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
                // 1. Validaciones iniciales
                if (logInspectionDto.LogProcess == null || logInspectionDto.LogProcess.IdLogReservation <= 0)
                {
                    return new OperationResponse<LogInspectionDto>(400,
                        "Se requiere información del LogProcess con una reserva válida");
                }

                // 2. Subir imagen principal si existe
                if (logInspectionDto.ImageFile != null)
                {
                    logInspectionDto.Image = await _s3Service.UploadImageAsync(logInspectionDto.ImageFile, "inspections");
                }

                // 3. Subir imágenes de las partes si existen
                if (logInspectionDto.PartsInspected != null)
                {
                    foreach (var part in logInspectionDto.PartsInspected)
                    {
                        if (part.ImageFile != null)
                        {
                            part.Image = await _s3Service.UploadImageAsync(part.ImageFile, "inspection-parts");
                        }
                    }
                }

                // 4. Crear el LogProcess
                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = logInspectionDto.LogProcess.IdLogReservation,
                    Action = logInspectionDto.LogProcess.Action ?? $"Creación de Inspección - {logInspectionDto.TypeInspection}",
                    IdCollaborator = logInspectionDto.IdCollaborator,
                    IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                };

                var processResponse = await _logProcessDao.CreateLogProcessAsync(logProcessDto);
                if (processResponse.Code != 200)
                {
                    return new OperationResponse<LogInspectionDto>(processResponse.Code,
                        $"Error creando LogProcess: {processResponse.Message}");
                }

                // 5. Crear la inspección
                var logInspection = new LogInspection
                {
                    IdCollaborator = logInspectionDto.IdCollaborator,
                    IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                    Comment = logInspectionDto.Comment,
                    Odometer = logInspectionDto.Odometer,
                    Fuel = logInspectionDto.Fuel,
                    TypeInspection = logInspectionDto.TypeInspection,
                    Status = logInspectionDto.Status,
                    CreationDate = DateTime.Now,
                    Image = logInspectionDto.Image,
                    IdLogProcess = processResponse.Data.IdLogProcess,
                    LogInspectionParts = logInspectionDto.PartsInspected?.Select(p => new LogInspectionPart
                    {
                        IdPartVehicle = p.IdPartVehicle,
                        Comment = p.Comment,
                        Status = p.Status,
                        Image = p.Image,
                        DateInspection = DateTime.Now
                    }).ToList()
                };

                var inspectionResponse = await _logInspectionDao.CreateLogInspectionAsync(logInspection);
                if (inspectionResponse.Code != 200)
                {
                    return new OperationResponse<LogInspectionDto>(inspectionResponse.Code,
                        inspectionResponse.Message);
                }

                // 6. Procesar las partes de la inspección
                if (logInspectionDto.PartsInspected != null && logInspectionDto.PartsInspected.Any())
                {
                    var parts = logInspectionDto.PartsInspected.Select(p => new LogInspectionPart
                    {
                        IdLogInspection = inspectionResponse.Data.IdLogInspection,
                        IdPartVehicle = p.IdPartVehicle,
                        Comment = p.Comment,
                        Status = p.Status,
                        Image = p.Image,
                        DateInspection = DateTime.Now
                    }).ToList();

                    var partsResponse = await _logInspectionDao.UpdateLogInspectionPartsAsync(
                        inspectionResponse.Data.IdLogInspection, parts);

                    if (partsResponse.Code != 200)
                    {
                        Console.WriteLine($"Warning: Error al actualizar partes: {partsResponse.Message}");
                    }

                    // 7. Procesar partes defectuosas
                    foreach (var part in logInspectionDto.PartsInspected.Where(p => !p.Status))
                    {
                        await _maintenancePartBao.SendPartToMaintenanceAsync(part.IdPartVehicle);
                        await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                    }
                }

                // 8. Verificar el odómetro
                if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
                {
                    await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                }

                // 9. Actualizar el LogProcess
                processResponse.Data.IdLogInspection = inspectionResponse.Data.IdLogInspection;
                await _logProcessDao.UpdateLogProcessAsync(processResponse.Data);

                var resultDto = MapToLogInspectionDto(inspectionResponse.Data);
                resultDto.LogProcess = processResponse.Data;

                return new OperationResponse<LogInspectionDto>(200, "Inspección creada exitosamente", resultDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500,
                    $"Error creando la inspección: {ex.Message} - Inner: {ex.InnerException?.Message}");
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
            try
            {
                // 1. Obtener la inspección existente
                var existingInspectionResponse = await _logInspectionDao.GetLogInspectionByIdAsync(id);
                if (existingInspectionResponse.Code != 200)
                {
                    return new OperationResponse<LogInspectionDto>(404, "Inspección no encontrada");
                }

                // 2. Manejar imagen principal
                if (logInspectionDto.ImageFile != null)
                {
                    // Eliminar imagen anterior si existe
                    if (!string.IsNullOrEmpty(existingInspectionResponse.Data.Image))
                    {
                        await _s3Service.DeleteImageAsync(existingInspectionResponse.Data.Image);
                    }
                    // Subir nueva imagen
                    logInspectionDto.Image = await _s3Service.UploadImageAsync(logInspectionDto.ImageFile, "inspections");
                }
                else
                {
                    // Mantener la imagen existente si no se proporciona una nueva
                    logInspectionDto.Image = existingInspectionResponse.Data.Image;
                }

                // 3. Manejar imágenes de las partes
                if (logInspectionDto.PartsInspected != null)
                {
                    foreach (var part in logInspectionDto.PartsInspected)
                    {
                        if (part.ImageFile != null)
                        {
                            // Buscar la parte existente
                            var existingPart = existingInspectionResponse.Data.LogInspectionParts?
                                .FirstOrDefault(p => p.IdPartVehicle == part.IdPartVehicle);

                            // Eliminar imagen anterior si existe
                            if (existingPart != null && !string.IsNullOrEmpty(existingPart.Image))
                            {
                                await _s3Service.DeleteImageAsync(existingPart.Image);
                            }

                            // Subir nueva imagen
                            part.Image = await _s3Service.UploadImageAsync(part.ImageFile, "inspection-parts");
                        }
                        else if (existingInspectionResponse.Data.LogInspectionParts != null)
                        {
                            // Mantener la imagen existente si no se proporciona una nueva
                            var existingPart = existingInspectionResponse.Data.LogInspectionParts
                                .FirstOrDefault(p => p.IdPartVehicle == part.IdPartVehicle);
                            if (existingPart != null)
                            {
                                part.Image = existingPart.Image;
                            }
                        }
                    }
                }

                // 4. Actualizar la inspección
                var logInspection = MapToLogInspection(logInspectionDto);
                logInspection.IdLogInspection = id;

                var updateResponse = await _logInspectionDao.UpdateLogInspectionAsync(id, logInspection);
                if (updateResponse.Code != 200)
                {
                    return new OperationResponse<LogInspectionDto>(updateResponse.Code, updateResponse.Message);
                }

                // 5. Verificar el odómetro
                if (int.TryParse(logInspectionDto.Odometer, out int currentOdometer) && currentOdometer >= 25000)
                {
                    await _vehicleBao.UpdateVehicleStatusAsync(logInspectionDto.IdVehicleAssignment, "En servicio");
                }

                var updatedLogInspectionDto = MapToLogInspectionDto(updateResponse.Data);
                return new OperationResponse<LogInspectionDto>(200, "Inspección actualizada exitosamente", updatedLogInspectionDto);
            }
            catch (Exception ex)
            {
                return new OperationResponse<LogInspectionDto>(500,
                    $"Error actualizando la inspección: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
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
        // Mapeo de DTO a entidad LogInspection
        private LogInspection MapToLogInspection(LogInspectionDto dto)
        {
            // Primero declaramos e inicializamos logInspection con las propiedades básicas
            var logInspection = new LogInspection
            {
                IdCollaborator = dto.IdCollaborator,
                IdVehicleAssignment = dto.IdVehicleAssignment,
                Comment = dto.Comment,
                Odometer = dto.Odometer,
                Fuel = dto.Fuel,
                TypeInspection = dto.TypeInspection,
                Status = dto.Status,
                CreationDate = dto.CreationDate,
                Image = dto.Image
            };

            // Ahora asignamos las partes inspeccionadas y establecemos el IdLogInspection de cada parte
            logInspection.LogInspectionParts = dto.PartsInspected?.Select(p => new LogInspectionPart
            {
                IdPartVehicle = p.IdPartVehicle,
                Comment = p.Comment,
                Status = p.Status,
                Image = p.Image,
                DateInspection = p.DateInspection,
                IdLogInspection = logInspection.IdLogInspection // Asignamos el Id de la inspección
            }).ToList();

            return logInspection;
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

        private LogInspectionDto MapToLogInspectionDtos(LogInspection entity)
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
                LogProcess = new LogProcessDto()
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
