using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class PartVehicleBaoService : IPartVehicleBao
    {
        private readonly IPartVehicleDao _partVehicleDao;

        public PartVehicleBaoService(IPartVehicleDao partVehicleDao)
        {
            _partVehicleDao = partVehicleDao;
        }

        public async Task<OperationResponse<PartVehicle>> CreatePartVehicleAsync(PartVehicle partVehicle)
        {

            return await _partVehicleDao.CreatePartVehicleAsync(partVehicle);
        }

        public async Task<OperationResponse<PartVehicle>> GetPartVehicleByIdAsync(int id)
        {
            return await _partVehicleDao.GetPartVehicleByIdAsync(id);
        }

        public async Task<OperationResponse<IEnumerable<PartVehicle>>> GetAllPartVehiclesAsync()
        {
            return await _partVehicleDao.GetAllPartVehiclesAsync();
        }

        public async Task<OperationResponse<PartVehicle>> UpdatePartVehicleAsync(PartVehicle partvehicle)
        {

            return await _partVehicleDao.UpdatePartVehicleAsync(partvehicle);
        }

        public async Task<OperationResponse<bool>> DeletePartVehicleAsync(int id)
        {
            return await _partVehicleDao.DeletePartVehicleAsync(id);
        }

    }
}
