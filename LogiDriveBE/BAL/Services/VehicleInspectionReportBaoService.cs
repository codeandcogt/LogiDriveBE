using LogiDriveBE.BAL.Bao;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Services
{
    public class VehicleInspectionReportBaoService : IVehicleInspectionReportBao
    {
        private readonly IVehicleInspectionReportDao _dao;

        public VehicleInspectionReportBaoService(IVehicleInspectionReportDao dao)
        {
            _dao = dao;
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionPdfReportAsync()
        {
            return await _dao.GenerateVehicleInspectionPdfReportAsync();
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionCsvReportAsync()
        {
            return await _dao.GenerateVehicleInspectionCsvReportAsync();
        }

        public async Task<OperationResponse<byte[]>> GenerateVehicleInspectionExcelReportAsync()
        {
            return await _dao.GenerateVehicleInspectionExcelReportAsync();
        }

        public async Task<List<VehicleInspectionReportDto>> GetVehicleInspectionReportAsync()
        {
            return await _dao.GetVehicleInspectionReportAsync();
        }
    }
}