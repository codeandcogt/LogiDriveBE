using LogiDriveBE.UTILS;
using LogiDriveBE.DAL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleAssignmentReportDao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentCsvReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentExcelReportAsync();
        Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync();
    }
}
