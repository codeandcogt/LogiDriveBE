using LogiDriveBE.UTILS;
using LogiDriveBE.DAL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleInspectionReportDao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleInspectionExcelReportAsync();
        Task<List<VehicleInspectionReportDto>> GetVehicleInspectionReportAsync();
    }
}
