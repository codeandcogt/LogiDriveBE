using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface IMaintenanceReportDao
    {
        Task<List<MaintenanceReportDto>> GetMaintenanceReportAsync();
        Task<byte[]> GenerateMaintenancePdfReportAsync();
        Task<byte[]> GenerateMaintenanceCsvReportAsync();
        Task<byte[]> GenerateMaintenanceExcelReportAsync();
    }
}
