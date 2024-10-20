using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class LogReservationBaoService : ILogReservationBao
    {
        private readonly ILogReservationDao _logReservationDao;
        private readonly ILogProcessDao _logProcessDao;

        public LogReservationBaoService(ILogReservationDao logReservationDao, ILogProcessDao logProcessDao)
        {
            _logReservationDao = logReservationDao;
            _logProcessDao = logProcessDao;
        }

        public async Task<OperationResponse<LogReservationDto>> CreateLogReservationAsync(LogReservationDto logReservationDto)
        {
            var response = await _logReservationDao.CreateLogReservationAsync(logReservationDto);

            if (response.Code == 200 && response.Data != null)
            {
                // Crear un LogProcess después de crear la reserva
                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = response.Data.IdLogReservation,
                    Action = "CREATE",
                    IdCollaborator = response.Data.IdCollaborator ?? 0 // Verificar si el colaborador es nulo
                };
                await _logProcessDao.CreateLogProcessAsync(logProcessDto);
            }

            return response;
        }

        public async Task<OperationResponse<LogReservationDto>> GetLogReservationByIdAsync(int id)
        {
            return await _logReservationDao.GetLogReservationByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<LogReservationDto>>> GetAllLogReservationsAsync()
        {
            return await _logReservationDao.GetAllLogReservationsAsync();
        }

        public async Task<OperationResponse<LogReservationDto>> UpdateLogReservationAsync(LogReservationDto logReservationDto)
        {
            var response = await _logReservationDao.UpdateLogReservationAsync(logReservationDto);

            if (response.Code == 200 && response.Data != null)
            {
                // Crear un LogProcess después de actualizar la reserva
                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = response.Data.IdLogReservation,
                    Action = "UPDATE",
                    IdCollaborator = response.Data.IdCollaborator ?? 0
                };
                await _logProcessDao.CreateLogProcessAsync(logProcessDto);
            }

            return response;
        }

        public async Task<OperationResponse<bool>> DeleteLogReservationAsync(int id)
        {
            return await _logReservationDao.DeleteLogReservationAsync(id);
        }

        public async Task<OperationResponse<bool>> UpdateStatusReservationAsync(int id, UpdateStatusReservationDto updateStatusReservationDto)
        {
            var response = await _logReservationDao.UpdateStatusReservationAsync(id, updateStatusReservationDto);

            if (response.Code == 200)
            {
                // Crear un LogProcess después de actualizar el estado de la reserva
                var logProcessDto = new LogProcessDto
                {
                    IdLogReservation = id,
                    Action = "UPDATE_STATUS",
                    IdCollaborator = 0 // No se especifica un colaborador para la actualización del estado
                };
                await _logProcessDao.CreateLogProcessAsync(logProcessDto);
            }

            return response;
        }
    }
}
