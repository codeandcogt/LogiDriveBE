using LogiDriveBE.DAL.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogiDriveBE.DAL.Dao
{
    public interface IVehicleProcessReservationReportDao
    {
        Task<List<ProcessReservationReportDto>> GetProcessReservationReportAsync();
        Task<byte[]> GenerateProcessReservationPdfReportAsync();
        Task<byte[]> GenerateProcessReservationCsvReportAsync();  // Nuevo método para CSV
        Task<byte[]> GenerateProcessReservationExcelReportAsync();  // Nuevo método para Excel
    }
}
