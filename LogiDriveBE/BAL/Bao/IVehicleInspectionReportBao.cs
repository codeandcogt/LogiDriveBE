using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleInspectionReportBao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionExcelReportAsync();
        Task<List<VehicleInspectionReportDto>> GetVehicleInspectionReportAsync();
    }
}
