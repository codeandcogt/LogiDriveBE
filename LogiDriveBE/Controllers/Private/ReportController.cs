using LogiDriveBE.BAL.Bao;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportBao _reportBao;

        public ReportController(IReportBao reportBao)
        {
            _reportBao = reportBao;
        }

        [HttpGet("generateReport")]
        public async Task<IActionResult> GenerateReport([FromQuery] string reportType)
        {
            var response = await _reportBao.GenerateReportAsync(reportType);

            if (response.Code == 200)
            {
                var file = response.Data;
                string mimeType = GetMimeType(reportType);
                return File(file, mimeType, $"report.{reportType}");
            }
            return StatusCode(response.Code, response);
        }

        private string GetMimeType(string reportType)
        {
            return reportType switch
            {
                "pdf" => "application/pdf",
                "csv" => "text/csv",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",
            };
        }
    }
}
