using System.Collections.Generic;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Models.DTO;

namespace LogiDriveBE.DAL.Dao
{
    public interface IProcessLogReportDao
    {
        Task<List<ProcessLogReportDto>> GetProcessLogReportAsync();
        Task<byte[]> GenerateProcessLogPdfReportAsync();
        Task<byte[]> GenerateProcessLogCsvReportAsync();
        Task<byte[]> GenerateProcessLogExcelReportAsync();
    }
}
