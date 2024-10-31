using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL.LogiDriveContext;
using System.IO;
using OfficeOpenXml;

namespace LogiDriveBE.DAL.Services
{
    public class VehicleProcessReservationReportDaoService : IVehicleProcessReservationReportDao
    {
        private readonly LogiDriveDbContext _context;

        public VehicleProcessReservationReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProcessReservationReportDto>> GetProcessReservationReportAsync()
        {
            return await _context.ProcessReservationReport.ToListAsync();
        }

        // Generar reporte en PDF
        public async Task<byte[]> GenerateProcessReservationPdfReportAsync()
        {
            var processReservations = await GetProcessReservationReportAsync();

            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Reporte de Procesos de Reservas")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold());

                Table table = new Table(5, true); // 5 columnas (ajustado)
                table.AddHeaderCell("ID Reserva");
                table.AddHeaderCell("Acción del Proceso");
                table.AddHeaderCell("Nombre del Colaborador");
                table.AddHeaderCell("Fecha del Proceso");
                table.AddHeaderCell("Vehículo Asignado");

                foreach (var process in processReservations)
                {
                    table.AddCell(process.ReservationID.ToString());
                    table.AddCell(process.ProcessAction);
                    table.AddCell(process.CollaboratorName);
                    table.AddCell(process.ProcessDate.ToString("dd/MM/yyyy"));
                    table.AddCell(process.VehicleAssigned);
                }

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }

        // Generar reporte en CSV
        public async Task<byte[]> GenerateProcessReservationCsvReportAsync()
        {
            var processReservations = await GetProcessReservationReportAsync();

            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("ID Reserva,Acción del Proceso,Nombre del Colaborador,Fecha del Proceso,Vehículo Asignado");

            foreach (var process in processReservations)
            {
                csvBuilder.AppendLine($"{process.ReservationID},{process.ProcessAction},{process.CollaboratorName},{process.ProcessDate:dd/MM/yyyy},{process.VehicleAssigned}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        // Generar reporte en Excel
        public async Task<byte[]> GenerateProcessReservationExcelReportAsync()
        {
            var processReservations = await GetProcessReservationReportAsync();

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Procesos de Reservas");

                // Encabezados de columnas
                worksheet.Cells[1, 1].Value = "ID Reserva";
                worksheet.Cells[1, 2].Value = "Acción del Proceso";
                worksheet.Cells[1, 3].Value = "Nombre del Colaborador";
                worksheet.Cells[1, 4].Value = "Fecha del Proceso";
                worksheet.Cells[1, 5].Value = "Vehículo Asignado";

                int row = 2;

                foreach (var process in processReservations)
                {
                    worksheet.Cells[row, 1].Value = process.ReservationID;
                    worksheet.Cells[row, 2].Value = process.ProcessAction;
                    worksheet.Cells[row, 3].Value = process.CollaboratorName;
                    worksheet.Cells[row, 4].Value = process.ProcessDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 5].Value = process.VehicleAssigned;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }
    }
}
