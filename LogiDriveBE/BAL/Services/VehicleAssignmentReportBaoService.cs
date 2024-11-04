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
            return await _vehicleAssignmentReportDao.GenerateVehicleAssignmentPdfReportAsync();
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentCsvReportAsync()
        {
            return await _vehicleAssignmentReportDao.GenerateVehicleAssignmentCsvReportAsync();
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleAssignmentExcelReportAsync()
        {
            return await _vehicleAssignmentReportDao.GenerateVehicleAssignmentExcelReportAsync();
        }

        public async Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync()
        {
            return await _vehicleAssignmentReportDao.GetVehicleAssignmentReportAsync();
        }
    }
}
