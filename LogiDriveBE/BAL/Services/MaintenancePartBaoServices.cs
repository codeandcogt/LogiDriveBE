using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class MaintenancePartBaoService : IMaintenancePartBao
    {
        private readonly IMaintenancePartDao _maintenancePartDao;

        public MaintenancePartBaoService(IMaintenancePartDao maintenancePartDao)
        {
            _maintenancePartDao = maintenancePartDao;
        }

        public async Task<OperationResponse<MaintenancePart>> CreateMaintenancePartAsync(MaintenancePart maintenancePart)
        {
            try
            {
               
                return await _maintenancePartDao.CreateMaintenancePartAsync(maintenancePart);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error in BAO while creating maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<MaintenancePart>> GetMaintenancePartAsync(int id)
        {
            try
            {
                return await _maintenancePartDao.GetMaintenancePartAsync(id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error in BAO while retrieving maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<IEnumerable<MaintenancePart>>> GetAllMaintenancePartAsync()
        {
            try
            {
                return await _maintenancePartDao.GetAllMaintenancePartAsync();
            }
            catch (Exception ex)
            {
                return new OperationResponse<IEnumerable<MaintenancePart>>(500, $"Error in BAO while retrieving all maintenance parts: {ex.Message}");
            }
        }

        public async Task<OperationResponse<MaintenancePart>> UpdateMaintenancePartAsync(MaintenancePart maintenancePart)
        {
            try
            {
                return await _maintenancePartDao.UpdateMaintenancePartAsync(maintenancePart);
            }
            catch (Exception ex)
            {
                return new OperationResponse<MaintenancePart>(500, $"Error in BAO while updating maintenance part: {ex.Message}");
            }
        }
        public async Task<OperationResponse<bool>> SendToMaintenanceAsync(int idPartVehicle)
        {
            try
            {
                // Lógica para enviar la parte a mantenimiento
                var maintenancePart = new MaintenancePart
                {
                    IdPartVehicle = idPartVehicle,
                    // Puedes agregar más propiedades aquí según sea necesario
                };

                var response = await CreateMaintenancePartAsync(maintenancePart); // Usar el método existente
                if (response.Code == 200)
                {
                    return new OperationResponse<bool>(200, "Part sent to maintenance successfully", true);
                }

                return new OperationResponse<bool>(500, "Error sending part to maintenance", false);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error: {ex.Message}", false);
            }
        }
        public async Task<OperationResponse<bool>> DeleteMaintenancePartAsync(int id)
        {
            try
            {
                return await _maintenancePartDao.DeleteMaintenancePartAsync(id);
            }
            catch (Exception ex)
            {
                return new OperationResponse<bool>(500, $"Error in BAO while deleting maintenance part: {ex.Message}");
            }
        }

        public async Task<OperationResponse<bool>> SendPartToMaintenanceAsync(int partId)
        {
            return await _maintenancePartDao.SendPartToMaintenanceAsync(partId);
        }
    }
}