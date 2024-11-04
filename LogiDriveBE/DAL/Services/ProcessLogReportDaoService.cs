using System.IO;
using System.Text;
using System.Threading.Tasks;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Models.DTO;
using LogiDriveBE.UTILS;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Collections.Generic;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Services
{
    public class ProcessLogReportDaoService : IProcessLogReportDao
    {
        private readonly LogiDriveDbContext _context;

        public ProcessLogReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProcessLogReportDto>> GetProcessLogReportAsync()
        {
            return await _context.Set<ProcessLogReportDto>().FromSqlRaw("SELECT * FROM vw_ProcessLogs").ToListAsync();
        }

        public async Task<byte[]> GenerateProcessLogPdfReportAsync()
        {
            var logs = await GetProcessLogReportAsync();
            using var memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            document.Add(new Paragraph("Reporte de Registros de Procesos")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold());

            Table table = new Table(5, true);
            table.AddHeaderCell("ID Proceso");
            table.AddHeaderCell("Colaborador");
            table.AddHeaderCell("Acción");
            table.AddHeaderCell("Fecha");
            table.AddHeaderCell("Estado Reserva");

            foreach (var log in logs)
            {
                table.AddCell(log.IdLogProcess.ToString());
                table.AddCell(log.CollaboratorName);
                table.AddCell(log.Action);
                table.AddCell(log.CreationDate.ToString("dd/MM/yyyy"));
                table.AddCell(log.ReservationStatus ? "Activo" : "Inactivo");
            }

            document.Add(table);
            document.Close();
            return memoryStream.ToArray();
        }

        public async Task<byte[]> GenerateProcessLogCsvReportAsync()
        {
            var logs = await GetProcessLogReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("ID Proceso,Colaborador,Acción,Fecha,Estado Reserva");

            foreach (var log in logs)
            {
                csvBuilder.AppendLine($"{log.IdLogProcess},{log.CollaboratorName},{log.Action},{log.CreationDate:dd/MM/yyyy},{(log.ReservationStatus ? "Activo" : "Inactivo")}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateProcessLogExcelReportAsync()
        {
            var logs = await GetProcessLogReportAsync();
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Registros de Procesos");

            worksheet.Cells[1, 1].Value = "ID Proceso";
            worksheet.Cells[1, 2].Value = "Colaborador";
            worksheet.Cells[1, 3].Value = "Acción";
            worksheet.Cells[1, 4].Value = "Fecha";
            worksheet.Cells[1, 5].Value = "Estado Reserva";

            int row = 2;
            foreach (var log in logs)
            {
                worksheet.Cells[row, 1].Value = log.IdLogProcess;
                worksheet.Cells[row, 2].Value = log.CollaboratorName;
                worksheet.Cells[row, 3].Value = log.Action;
                worksheet.Cells[row, 4].Value = log.CreationDate.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 5].Value = log.ReservationStatus ? "Activo" : "Inactivo";
                row++;
            }

            return package.GetAsByteArray();
        }
    }
}
