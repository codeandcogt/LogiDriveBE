using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleAssignmentReportBao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentPdfReportAsync();
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentCsvReportAsync();  // Agregar soporte para CSV
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentExcelReportAsync(); // Agregar soporte para Excel
        Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync();
    }
}
