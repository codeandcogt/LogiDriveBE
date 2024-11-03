using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.DAL.LogiDriveContext;
using Microsoft.EntityFrameworkCore;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using OfficeOpenXml;

namespace LogiDriveBE.DAL.Services
{
    public class LogTripReportDaoService : ILogTripReportDao
    {
        private readonly LogiDriveDbContext _context;

        public LogTripReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogTripReportDto>> GetLogTripReportAsync()
        {
            return await _context.LogTripReport.ToListAsync();
        }

        public async Task<byte[]> GenerateLogTripPdfReportAsync()
        {
            var reportData = await GetLogTripReportAsync();
            using (var memoryStream = new MemoryStream())
            {
                var writer = new PdfWriter(memoryStream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Viajes").SetFontSize(20).SetBold());

                var table = new Table(7, true);
                table.AddHeaderCell("Trip Date");
                table.AddHeaderCell("Activity Type");
                table.AddHeaderCell("Trip Type");
                table.AddHeaderCell("Vehicle Brand");
                table.AddHeaderCell("Vehicle Plate");
                table.AddHeaderCell("Vehicle Type");
                table.AddHeaderCell("Assignment Comment");

                foreach (var item in reportData)
                {
                    table.AddCell(item.TripDateHour.ToString("dd/MM/yyyy"));
                    table.AddCell(item.ActivityType);
                    table.AddCell(item.TripType);
                    table.AddCell(item.VehicleBrand);
                    table.AddCell(item.VehiclePlate);
                    table.AddCell(item.VehicleType);
                    table.AddCell(item.AssignmentComment ?? "N/A");
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        public async Task<byte[]> GenerateLogTripCsvReportAsync()
        {
            var reportData = await GetLogTripReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Trip Date,Activity Type,Trip Type,Vehicle Brand,Vehicle Plate,Vehicle Type,Assignment Comment");

            foreach (var item in reportData)
            {
                csvBuilder.AppendLine($"{item.TripDateHour:dd/MM/yyyy},{item.ActivityType},{item.TripType},{item.VehicleBrand},{item.VehiclePlate},{item.VehicleType},{item.AssignmentComment ?? "N/A"}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateLogTripExcelReportAsync()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var reportData = await GetLogTripReportAsync();
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Log Trip Report");

                worksheet.Cells[1, 1].Value = "Trip Date";
                worksheet.Cells[1, 2].Value = "Activity Type";
                worksheet.Cells[1, 3].Value = "Trip Type";
                worksheet.Cells[1, 4].Value = "Vehicle Brand";
                worksheet.Cells[1, 5].Value = "Vehicle Plate";
                worksheet.Cells[1, 6].Value = "Vehicle Type";
                worksheet.Cells[1, 7].Value = "Assignment Comment";

                int row = 2;
                foreach (var item in reportData)
                {
                    worksheet.Cells[row, 1].Value = item.TripDateHour.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 2].Value = item.ActivityType;
                    worksheet.Cells[row, 3].Value = item.TripType;
                    worksheet.Cells[row, 4].Value = item.VehicleBrand;
                    worksheet.Cells[row, 5].Value = item.VehiclePlate;
                    worksheet.Cells[row, 6].Value = item.VehicleType;
                    worksheet.Cells[row, 7].Value = item.AssignmentComment ?? "N/A";
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
