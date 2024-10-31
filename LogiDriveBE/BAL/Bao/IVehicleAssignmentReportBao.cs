using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.BAL.Bao
{
    public interface IVehicleAssignmentReportBao
    {
        Task<OperationResponse<byte[]>> GenerateVehicleAssignmentPdfReportAsync();
        Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync();
    }
}
