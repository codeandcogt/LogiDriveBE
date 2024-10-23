using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models;

namespace LogiDriveBE.BAL.Services
{
    public class LogInspectionBaoService : ILogInspectionBao
    {
        private readonly ILogInspectionDao _logInspectionDao;
        private readonly IVehicleBao _vehicleBao; // To update vehicle status
        private readonly ILogProcessBao _logProcessBao; // To create LogProcess
        private readonly IMaintenancePartBao _maintenancePartBao; // To send defective parts to maintenance

        public LogInspectionBaoService(ILogInspectionDao logInspectionDao, IVehicleBao vehicleBao, ILogProcessBao logProcessBao, IMaintenancePartBao maintenancePartBao)
        {
            _logInspectionDao = logInspectionDao;
            _vehicleBao = vehicleBao;
            _logProcessBao = logProcessBao;
            _maintenancePartBao = maintenancePartBao;
        }

        // Method to create an inspection and validate the flow
        public async Task<OperationResponse<LogInspectionDto>> CreateLogInspectionAsync(LogInspectionDto logInspectionDto)
        {
            // Check if it's a reception inspection
            if (logInspectionDto.TypeInspection == "recepcion")
            {
                // Look for a previous delivery inspection for the same IdVehicleAssignment
                var existingInspection = await _logInspectionDao.GetLogInspectionByVehicleAssignmentAndTypeAsync(
                    logInspectionDto.IdVehicleAssignment.Value, "entrega");

                if (existingInspection.Data == null)
                {
                    // If there's no delivery inspection, return an error
                    return new OperationResponse<LogInspectionDto>(400, "Cannot perform a reception inspection without a prior delivery.");
                }
            }

            // Create the inspection
            var response = await _logInspectionDao.CreateLogInspectionAsync(logInspectionDto);

            if (response.Code == 200 && response.Data != null)
            {
                // Check if any part is defective
                bool hasDefectiveParts = logInspectionDto.PartsInspected.Any(part => part.Status == false);

                if (hasDefectiveParts)
                {
                    // Change vehicle status to "in service"
                    await _vehicleBao.UpdateVehicleStatusAsync(response.Data.IdVehicleAssignment.Value, "en servicio");
                }
                else
                {
                    // Change vehicle status to "available"
                    await _vehicleBao.UpdateVehicleStatusAsync(response.Data.IdVehicleAssignment.Value, "disponible");
                }

                // Verify odometer limit for service (25,000 km)
                if (int.Parse(logInspectionDto.Odometer) >= 25000)
                {
                    // If the odometer exceeds the limit, change vehicle status to "in service"
                    await _vehicleBao.UpdateVehicleStatusAsync(response.Data.IdVehicleAssignment.Value, "en servicio");
                }

                // Create a LogProcess to record the inspection
                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = response.Data.IdLogReservation,
                    Action = "CREATE_INSPECTION",
                    IdCollaborator = logInspectionDto.IdCollaborator,
                    IdVehicleAssignment = logInspectionDto.IdVehicleAssignment,
                    IdLogInspection = response.Data.IdLogInspection
                };

                await _logProcessBao.CreateLogProcessAsync(logProcessDto);

                // Send defective parts to maintenance
                foreach (var partDto in logInspectionDto.PartsInspected)
                {
                    if (!partDto.Status) // If the part is defective
                    {
                        var maintenancePart = new MaintenancePart
                        {
                            IdPartVehicle = partDto.IdPartVehicle,
                            Comment = partDto.Comment,
                            Status = false,
                            DateMaintenancePart = DateTime.Now
                        };

                        await _maintenancePartBao.CreateMaintenancePartAsync(maintenancePart);
                    }
                }
            }

            return response;
        }

        public async Task<OperationResponse<LogInspectionDto>> GetLogInspectionByIdAsync(int id)
        {
            return await _logInspectionDao.GetLogInspectionByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogInspectionDto>>> GetAllLogInspectionsAsync()
        {
            return await _logInspectionDao.GetAllLogInspectionsAsync();
        }

        public async Task<OperationResponse<LogInspectionDto>> UpdateLogInspectionAsync(int id, LogInspectionDto logInspectionDto)
        {
            return await _logInspectionDao.UpdateLogInspectionAsync(id, logInspectionDto);
        }

        public async Task<OperationResponse<bool>> DeleteLogInspectionAsync(int id)
        {
            return await _logInspectionDao.DeleteLogInspectionAsync(id);
        }

        // Method to get inspection by VehicleAssignment and inspection type
        public async Task<OperationResponse<LogInspection>> GetLogInspectionByVehicleAssignmentAndTypeAsync(int idVehicleAssignment, string typeInspection)
        {
            return await _logInspectionDao.GetLogInspectionByVehicleAssignmentAndTypeAsync(idVehicleAssignment, typeInspection);
        }
    }
}
