using LogiDriveBE.BAL.Bao;
using LogiDriveBE.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LogiDriveBE.Controllers.Private
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportBao _reportBao;
        private readonly IVehicleAssignmentReportBao _vehicleAssignmentReportBao;
        private readonly IVehicleProcessReservationReportBao _vehicleProcessReservationReportBao;
        private readonly IVehicleInspectionReportBao _vehicleInspectionReportBao;
        private readonly IProcessLogReportBao _processLogReportBao;

        public ReportController(
            IReportBao reportBao,
            IVehicleAssignmentReportBao vehicleAssignmentReportBao,
            IVehicleProcessReservationReportBao vehicleProcessReservationReportBao,
            IVehicleInspectionReportBao vehicleInspectionReportBao,
            IProcessLogReportBao processLogReportBao)
        {
            _reportBao = reportBao;
            _vehicleAssignmentReportBao = vehicleAssignmentReportBao;
            _vehicleProcessReservationReportBao = vehicleProcessReservationReportBao;
            _vehicleInspectionReportBao = vehicleInspectionReportBao;
            _processLogReportBao = processLogReportBao;
        }

        [HttpGet("generateReport")]
        public async Task<IActionResult> GenerateReport([FromQuery] string reportType)
        {
            byte[] reportBytes;

            switch (reportType)
            {
                case "collaboratorPdf":
                    var response = await _reportBao.GenerateReportAsync("pdf");
                    if (response.Code == 200)
                    {
                        reportBytes = response.Data;
                        string mimeType = GetMimeType(reportType);
                        return File(reportBytes, mimeType, $"report_{reportType}.pdf");
                    }
                    return StatusCode(response.Code, response);

                case "assignedVehiclesPdf":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentPdfReportAsync()).Data;
                    string assignedVehicleMimeType = GetMimeType(reportType);
                    return File(reportBytes, assignedVehicleMimeType, $"reporte_{reportType}.pdf");

                case "processReservationPdf":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationPdfReportAsync()).Data;
                    string processReservationMimeType = GetMimeType(reportType);
                    return File(reportBytes, processReservationMimeType, $"reporte_{reportType}.pdf");

                case "inspectionPdf":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionPdfReportAsync()).Data;
                    string inspectionPdfMimeType = GetMimeType(reportType);
                    return File(reportBytes, inspectionPdfMimeType, $"reporte_{reportType}.pdf");

                case "processLogPdf":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogPdfReportAsync()).Data;
                    string processLogPdfMimeType = GetMimeType(reportType);
                    return File(reportBytes, processLogPdfMimeType, $"reporte_{reportType}.pdf");

                case "collaboratorCsv":
                    var csvResponse = await _reportBao.GenerateReportAsync("csv");
                    if (csvResponse.Code == 200)
                    {
                        reportBytes = csvResponse.Data;
                        string mimeType = GetMimeType(reportType);
                        return File(reportBytes, mimeType, $"report_{reportType}.csv");
                    }
                    return StatusCode(csvResponse.Code, csvResponse);

                case "assignedVehiclesCsv":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentCsvReportAsync()).Data;
                    string assignedVehicleCsvMimeType = GetMimeType(reportType);
                    return File(reportBytes, assignedVehicleCsvMimeType, $"reporte_{reportType}.csv");

                case "processReservationCsv":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationCsvReportAsync()).Data;
                    string processReservationCsvMimeType = GetMimeType(reportType);
                    return File(reportBytes, processReservationCsvMimeType, $"reporte_{reportType}.csv");

                case "inspectionCsv":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionCsvReportAsync()).Data;
                    string inspectionCsvMimeType = GetMimeType(reportType);
                    return File(reportBytes, inspectionCsvMimeType, $"reporte_{reportType}.csv");

                case "processLogCsv":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogCsvReportAsync()).Data;
                    string processLogCsvMimeType = GetMimeType(reportType);
                    return File(reportBytes, processLogCsvMimeType, $"reporte_{reportType}.csv");

                case "collaboratorExcel":
                    var excelResponse = await _reportBao.GenerateReportAsync("xlsx");
                    if (excelResponse.Code == 200)
                    {
                        reportBytes = excelResponse.Data;
                        string mimeType = GetMimeType(reportType);
                        return File(reportBytes, mimeType, $"report_{reportType}.xlsx");
                    }
                    return StatusCode(excelResponse.Code, excelResponse);

                case "assignedVehiclesExcel":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentExcelReportAsync()).Data;
                    string assignedVehicleExcelMimeType = GetMimeType(reportType);
                    return File(reportBytes, assignedVehicleExcelMimeType, $"reporte_{reportType}.xlsx");

                case "processReservationExcel":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationExcelReportAsync()).Data;
                    string processReservationExcelMimeType = GetMimeType(reportType);
                    return File(reportBytes, processReservationExcelMimeType, $"reporte_{reportType}.xlsx");

                case "inspectionExcel":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionExcelReportAsync()).Data;
                    string inspectionExcelMimeType = GetMimeType(reportType);
                    return File(reportBytes, inspectionExcelMimeType, $"reporte_{reportType}.xlsx");

                case "processLogExcel":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogExcelReportAsync()).Data;
                    string processLogExcelMimeType = GetMimeType(reportType);
                    return File(reportBytes, processLogExcelMimeType, $"reporte_{reportType}.xlsx");

                default:
                    return BadRequest("Tipo de reporte no válido.");
            }
        }

        private string GetMimeType(string reportType)
        {
            return reportType switch
            {
                "pdf" => "application/pdf",
                "collaboratorPdf" => "application/pdf",
                "assignedVehiclesPdf" => "application/pdf",
                "processReservationPdf" => "application/pdf",
                "inspectionPdf" => "application/pdf",
                "processLogPdf" => "application/pdf",
                "csv" => "text/csv",
                "collaboratorCsv" => "text/csv",
                "assignedVehiclesCsv" => "text/csv",
                "processReservationCsv" => "text/csv",
                "inspectionCsv" => "text/csv",
                "processLogCsv" => "text/csv",
                "xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "collaboratorExcel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "assignedVehiclesExcel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "processReservationExcel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "inspectionExcel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "processLogExcel" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream",
            };
        }
    }
}
