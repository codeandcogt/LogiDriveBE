using LogiDriveBE.DAL.Models;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Bao
{
    public interface IServiceBao
    {
        Task<OperationResponse<Service>> CreateServiceAsync(Service service);
        Task<OperationResponse<Service>> GetServiceAsync(int idService);
        Task<OperationResponse<IEnumerable<Service>>> GetAllServicesAsync();
        Task<OperationResponse<IEnumerable<Service>>> GetServicesByVehicleAsync(int idVehicle);
        Task<OperationResponse<IEnumerable<Service>>> GetServicesByTypeAsync(int idTypeService);
        Task<OperationResponse<Service>> UpdateServiceAsync(Service service);
        Task<OperationResponse<bool>> DeleteServiceAsync(int idService);
        Task<OperationResponse<bool>> ChangeServiceStatusAsync(int idService, bool status);

    }
}
