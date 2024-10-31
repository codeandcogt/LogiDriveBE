using System.Text;
using OfficeOpenXml;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.UTILS;
using LogiDriveBE.DAL.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.IO;
using LogiDriveBE.DAL.LogiDriveContext;

namespace LogiDriveBE.DAL.Services
{
    public class ReportDaoService : IReportDao
    {
        private readonly LogiDriveDbContext _context;

        public ReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResponse<byte[]>> GenerateReportAsync(string reportType)
        {
            byte[] reportBytes = null;
            switch (reportType)
            {
                case "pdf":
                    reportBytes = await GeneratePdfReportAsync();
                    break;
                case "csv":
                    reportBytes = await GenerateCsvReportAsync();
                    break;
                case "xlsx":
                    reportBytes = await GenerateExcelReportAsync();
                    break;
                default:
                    return new OperationResponse<byte[]>(400, "Invalid report type");
            }

            return new OperationResponse<byte[]>(200, "Report generated successfully", reportBytes);
        }

        private async Task<byte[]> GeneratePdfReportAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Agregar un título
                document.Add(new Paragraph("Reporte de Colaboradores y Usuarios")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold());

                // Crear una tabla con las columnas necesarias
                Table table = new Table(7, true); // 7 columnas
                table.AddHeaderCell("Colaborador");
                table.AddHeaderCell("Apellido");
                table.AddHeaderCell("Posición");
                table.AddHeaderCell("Teléfono");
                table.AddHeaderCell("Área");
                table.AddHeaderCell("Usuario");
                table.AddHeaderCell("Rol");

                // Obtener datos de colaboradores
                var collaborators = await GetReportCollaboratorsAsync();

                foreach (var collaborator in collaborators)
                {
                    table.AddCell(collaborator.CollaboratorName);
                    table.AddCell(collaborator.LastName);
                    table.AddCell(collaborator.Position);
                    table.AddCell(collaborator.Phone);
                    table.AddCell(collaborator.AreaName);
                    table.AddCell(collaborator.AppUserName);
                    table.AddCell(collaborator.RoleName);
                }

                // Añadir la tabla al documento
                document.Add(table);

                // Cerrar el documento
                document.Close();
                return memoryStream.ToArray();
            }
        }

        private async Task<byte[]> GenerateCsvReportAsync()
        {
            var csvContent = new StringBuilder();
            csvContent.AppendLine("Colaborador,Apellido,Posición,Teléfono,Área,Usuario,Rol");

            var collaborators = await GetReportCollaboratorsAsync();
            foreach (var collaborator in collaborators)
            {
                csvContent.AppendLine($"{collaborator.CollaboratorName},{collaborator.LastName},{collaborator.Position},{collaborator.Phone},{collaborator.AreaName},{collaborator.AppUserName},{collaborator.RoleName}");
            }

            return Encoding.UTF8.GetBytes(csvContent.ToString());
        }

        private async Task<byte[]> GenerateExcelReportAsync()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte de Colaboradores");
                worksheet.Cells[1, 1].Value = "Colaborador";
                worksheet.Cells[1, 2].Value = "Apellido";
                worksheet.Cells[1, 3].Value = "Posición";
                worksheet.Cells[1, 4].Value = "Teléfono";
                worksheet.Cells[1, 5].Value = "Área";
                worksheet.Cells[1, 6].Value = "Usuario";
                worksheet.Cells[1, 7].Value = "Rol";

                var collaborators = await GetReportCollaboratorsAsync();
                int row = 2;
                foreach (var collaborator in collaborators)
                {
                    worksheet.Cells[row, 1].Value = collaborator.CollaboratorName;
                    worksheet.Cells[row, 2].Value = collaborator.LastName;
                    worksheet.Cells[row, 3].Value = collaborator.Position;
                    worksheet.Cells[row, 4].Value = collaborator.Phone;
                    worksheet.Cells[row, 5].Value = collaborator.AreaName;
                    worksheet.Cells[row, 6].Value = collaborator.AppUserName;
                    worksheet.Cells[row, 7].Value = collaborator.RoleName;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public async Task<List<ReportCollaboratorDto>> GetReportCollaboratorsAsync()
        {
            var collaborators = await (from user in _context.AppUsers
                                       join collaborator in _context.Collaborators on user.IdAppUser equals collaborator.IdUser
                                       join area in _context.Areas on collaborator.IdArea equals area.IdArea
                                       join role in _context.Roles on user.IdRole equals role.IdRole
                                       where user.Status == true && collaborator.Status == true
                                       select new ReportCollaboratorDto
                                       {
                                           CollaboratorName = collaborator.Name,
                                           LastName = collaborator.LastName,
                                           Position = collaborator.Position,
                                           Phone = collaborator.Phone,
                                           AreaName = area.Name,
                                           AppUserName = user.Name,
                                           AppUserEmail = user.Email,
                                           RoleName = role.Name
                                       }).ToListAsync();

            return collaborators;
        }
    }
}
