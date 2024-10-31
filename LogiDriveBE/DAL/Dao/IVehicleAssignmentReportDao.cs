using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleAssignmentReportDao
    {
        Task<byte[]> GenerateVehicleAssignmentPdfReportAsync();
        Task<List<VehicleAssignmentReportDto>> GetVehicleAssignmentReportAsync();
    }
}
