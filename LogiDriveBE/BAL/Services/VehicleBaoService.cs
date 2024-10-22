using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleBaoService : IVehicleBao
    {
        private readonly IVehicleDao _vehicleDao;

        public VehicleBaoService(IVehicleDao vehicleDao)
        {
            _vehicleDao = vehicleDao;
        }

        public async Task<OperationResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
           
            return await _vehicleDao.CreateVehicleAsync(vehicle);
        }

        public async Task<OperationResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            return await _vehicleDao.GetVehicleByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<Vehicle>>> GetAllVehiclesAsync()
        {
            return await _vehicleDao.GetAllVehiclesAsync();
        }

        public async Task<OperationResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            
            return await _vehicleDao.UpdateVehicleAsync(vehicle);
        }

        public async Task<OperationResponse<bool>> DeleteVehicleAsync(int id)
        {
            return await _vehicleDao.DeleteVehicleAsync(id);
        }

        public async Task<OperationResponse<bool>> DeleteVehicleStatusAsync(int id)
        {
            return await _vehicleDao.DeleteVehicleStatusAsync(id);
        }

    }

}
