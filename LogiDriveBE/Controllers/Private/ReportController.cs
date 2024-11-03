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
        private readonly IUserRolePermissionReportBao _userRolePermissionReportBao;
        private readonly IActivityByCollaboratorReportBao _activityByCollaboratorReportBao;
        private readonly ILogTripReportBao _logTripReportBao;
        private readonly IVehicleAvailabilityReportBao _vehicleAvailabilityReportBao;

        public ReportController(
            IReportBao reportBao,
            IVehicleAssignmentReportBao vehicleAssignmentReportBao,
            IVehicleProcessReservationReportBao vehicleProcessReservationReportBao,
            IVehicleInspectionReportBao vehicleInspectionReportBao,
            IProcessLogReportBao processLogReportBao,
            IUserRolePermissionReportBao userRolePermissionReportBao,
            IActivityByCollaboratorReportBao activityByCollaboratorReportBao,
            ILogTripReportBao logTripReportBao,
            IVehicleAvailabilityReportBao vehicleAvailabilityReportBao)
        {
            _reportBao = reportBao;
            _vehicleAssignmentReportBao = vehicleAssignmentReportBao;
            _vehicleProcessReservationReportBao = vehicleProcessReservationReportBao;
            _vehicleInspectionReportBao = vehicleInspectionReportBao;
            _processLogReportBao = processLogReportBao;
            _userRolePermissionReportBao = userRolePermissionReportBao;
            _activityByCollaboratorReportBao = activityByCollaboratorReportBao;
            _logTripReportBao = logTripReportBao;
            _vehicleAvailabilityReportBao = vehicleAvailabilityReportBao;
        }

        [HttpGet("generateReport")]
        public async Task<IActionResult> GenerateReport([FromQuery] string reportType)
        {
            byte[] reportBytes;
            string mimeType;
            string fileExtension;

            switch (reportType)
            {
                case "activityByCollaboratorExcel":
                    reportBytes = (await _activityByCollaboratorReportBao.GenerateActivityByCollaboratorExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "activityByCollaboratorPdf":
                    reportBytes = (await _activityByCollaboratorReportBao.GenerateActivityByCollaboratorPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "activityByCollaboratorCsv":
                    reportBytes = (await _activityByCollaboratorReportBao.GenerateActivityByCollaboratorCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                // Reportes de viajes (LogTrip)
                case "logTripExcel":
                    reportBytes = (await _logTripReportBao.GenerateLogTripExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "logTripPdf":
                    reportBytes = (await _logTripReportBao.GenerateLogTripPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "logTripCsv":
                    reportBytes = (await _logTripReportBao.GenerateLogTripCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                // Reportes de disponibilidad de vehículos
                case "vehicleAvailabilityExcel":
                    reportBytes = (await _vehicleAvailabilityReportBao.GenerateVehicleAvailabilityExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "vehicleAvailabilityPdf":
                    reportBytes = (await _vehicleAvailabilityReportBao.GenerateVehicleAvailabilityPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "vehicleAvailabilityCsv":
                    reportBytes = (await _vehicleAvailabilityReportBao.GenerateVehicleAvailabilityCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "collaboratorPdf":
                    var pdfResponse = await _reportBao.GenerateReportAsync("pdf");
                    if (pdfResponse.Code == 200)
                    {
                        reportBytes = pdfResponse.Data;
                        mimeType = "application/pdf";
                        fileExtension = "pdf";
                        return File(reportBytes, mimeType, $"report_{reportType}.{fileExtension}");
                    }
                    return StatusCode(pdfResponse.Code, pdfResponse);

                case "assignedVehiclesPdf":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "processReservationPdf":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "inspectionPdf":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "processLogPdf":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "userRolePermissionPdf":
                    reportBytes = (await _userRolePermissionReportBao.GenerateUserRolePermissionPdfReportAsync()).Data;
                    mimeType = "application/pdf";
                    fileExtension = "pdf";
                    break;

                case "collaboratorCsv":
                    var csvResponse = await _reportBao.GenerateReportAsync("csv");
                    if (csvResponse.Code == 200)
                    {
                        reportBytes = csvResponse.Data;
                        mimeType = "text/csv";
                        fileExtension = "csv";
                        return File(reportBytes, mimeType, $"report_{reportType}.{fileExtension}");
                    }
                    return StatusCode(csvResponse.Code, csvResponse);

                case "assignedVehiclesCsv":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "processReservationCsv":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "inspectionCsv":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "processLogCsv":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "userRolePermissionCsv":
                    reportBytes = (await _userRolePermissionReportBao.GenerateUserRolePermissionCsvReportAsync()).Data;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;

                case "collaboratorExcel":
                    var excelResponse = await _reportBao.GenerateReportAsync("xlsx");
                    if (excelResponse.Code == 200)
                    {
                        reportBytes = excelResponse.Data;
                        mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        fileExtension = "xlsx";
                        return File(reportBytes, mimeType, $"report_{reportType}.{fileExtension}");
                    }
                    return StatusCode(excelResponse.Code, excelResponse);

                case "assignedVehiclesExcel":
                    reportBytes = (await _vehicleAssignmentReportBao.GenerateVehicleAssignmentExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "processReservationExcel":
                    reportBytes = (await _vehicleProcessReservationReportBao.GenerateProcessReservationExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "inspectionExcel":
                    reportBytes = (await _vehicleInspectionReportBao.GenerateVehicleInspectionExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "processLogExcel":
                    reportBytes = (await _processLogReportBao.GenerateProcessLogExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                case "userRolePermissionExcel":
                    reportBytes = (await _userRolePermissionReportBao.GenerateUserRolePermissionExcelReportAsync()).Data;
                    mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;

                default:
                    return BadRequest("Tipo de reporte no válido.");
            }

            return File(reportBytes, mimeType, $"reporte_{reportType}.{fileExtension}");
        }
    }
}
