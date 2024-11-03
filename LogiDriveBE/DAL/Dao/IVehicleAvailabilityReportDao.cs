using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleAvailabilityReportDao
    {
        Task<List<VehicleAvailabilityReportDto>> GetVehicleAvailabilityReportAsync();
        Task<byte[]> GenerateVehicleAvailabilityPdfReportAsync();
        Task<byte[]> GenerateVehicleAvailabilityCsvReportAsync();
        Task<byte[]> GenerateVehicleAvailabilityExcelReportAsync();
    }
}
