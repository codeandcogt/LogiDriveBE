using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface ILogTripReportDao
    {
        Task<List<LogTripReportDto>> GetLogTripReportAsync();
        Task<byte[]> GenerateLogTripPdfReportAsync();
        Task<byte[]> GenerateLogTripCsvReportAsync();
        Task<byte[]> GenerateLogTripExcelReportAsync();
    }
}
