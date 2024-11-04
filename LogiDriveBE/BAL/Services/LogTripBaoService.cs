using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogTripBaoService : ILogTripBao
    {
        private readonly ILogTripDao _logTripDao;
        private readonly ILogTrackingDao _logTrackingDao;

        public LogTripBaoService(ILogTripDao logTripDao, ILogTrackingDao logTrackingDao)
        {
            _logTripDao = logTripDao;
            _logTrackingDao = logTrackingDao;
        }

        public async Task<OperationResponse<LogTrip>> CreateLogTripWithActivityAsync(LogTrip logTrip)
        {
            // Paso 1: Crear el LogTrip sin IdTracking
            var logTripResponse = await _logTripDao.CreateLogTripAsync(logTrip);

            if (logTripResponse.Code != 200 || logTripResponse.Data == null)
            {
                return logTripResponse; // Si falla la creación de LogTrip, retorna el error.
            }

            if (logTrip.ActivityType == "salida")
            {
                // Paso 2: Crear TrackingDto y asociarlo con el IdLogTrip recién creado
                var trackingDto = new TrakingDto
                {
                    Latitude = 0, // Reemplaza con los valores necesarios
                    Longitude = 0, // Reemplaza con los valores necesarios
                    Status = true,
                    IdLogTrip = logTripResponse.Data.IdLogTrip // Asociar con el LogTrip recién creado
                };

                var logTrackingResponse = await _logTrackingDao.CreateLogTrackingAsync(trackingDto);

                if (logTrackingResponse.Code != 200 || logTrackingResponse.Data == null)
                {
                    return new OperationResponse<LogTrip>(500, "Error creating LogTracking after LogTrip creation", null);
                }

                // Paso 3: Actualizar LogTrip con el IdTracking del LogTracking recién creado
                logTrip.IdTracking = logTrackingResponse.Data.IdTracking;
                var updateTripResponse = await _logTripDao.UpdateLogTripAsync(logTrip);

                if (updateTripResponse.Code != 200)
                {
                    return new OperationResponse<LogTrip>(500, "Error updating LogTrip with new LogTracking ID", null);
                }

                return updateTripResponse;
            }
            else if (logTrip.ActivityType == "entrada")
            {
                // Buscar el LogTracking activo para el vehículo y marcarlo como cerrado
                var activeTrackingResponse = await _logTrackingDao.GetActiveLogTrackingByVehicleAssignmentIdAsync(logTrip.IdVehicleAssignment);

                if (activeTrackingResponse.Code == 200 && activeTrackingResponse.Data != null)
                {
                    var activeLogTracking = activeTrackingResponse.Data;
                    activeLogTracking.Status = false; // Marcar el LogTracking como cerrado

                    var updateTrackingResponse = await _logTrackingDao.UpdateLogTrackingAsync(activeLogTracking);

                    if (updateTrackingResponse.Code != 200)
                    {
                        return new OperationResponse<LogTrip>(500, "Error updating LogTracking to closed status", null);
                    }
                }
                else
                {
                    return new OperationResponse<LogTrip>(404, "No active log tracking found for closing", null);
                }
            }

            return logTripResponse;
        }





        public async Task<OperationResponse<LogTrip>> UpdateLogTripAsync(LogTrip logTrip)
        {
            return await _logTripDao.UpdateLogTripAsync(logTrip);
        }

        public async Task<OperationResponse<bool>> DeleteLogTripAsync(int id)
        {
            return await _logTripDao.DeleteLogTripAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteLogTripStatusAsync(int id)
        {
            return await _logTripDao.DeleteLogTripStatusAsync(id);
        }

        public async Task<OperationResponse<LogTrip>> GetLogTripByIdAsync(int id)
        {
            return await _logTripDao.GetLogTripByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogTrip>>> GetAllLogTripsAsync()
        {
            return await _logTripDao.GetAllLogTripsAsync();
        }
    }
}
