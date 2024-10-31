using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleAssignmentReportBaoService : IVehicleAssignmentReportBao
    {
        private readonly IVehicleAssignmentReportDao _vehicleAssignmentReportDao;

        public VehicleAssignmentReportBaoService(IVehicleAssignmentReportDao vehicleAssignmentReportDao)
        {
            _vehicleAssignmentReportDao = vehicleAssignmentReportDao;
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentPdfReportAsync()
        {
            var reportData = await _vehicleAssignmentReportDao.GenerateVehicleAssignmentPdfReportAsync();
            return new OperationResponse<byte[]>(200, "Reporte generado exitosamente", reportData);
        }

        public async Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync()
        {
            return await _vehicleAssignmentReportDao.GetVehicleAssignmentReportAsync();
        }
    }
}
