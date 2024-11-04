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
    public class UserRolePermissionReportDaoService : IUserRolePermissionReportDao
    {
        private readonly LogiDriveDbContext _context;

        public UserRolePermissionReportDaoService(LogiDriveDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRolePermissionReportDto>> GetUserRolePermissionReportAsync()
        {
            return await _context.UserRolePermissions.FromSqlRaw("SELECT * FROM vw_UserRolesPermissions").ToListAsync();
        }

        public async Task<byte[]> GenerateUserRolePermissionPdfReportAsync()
        {
            var data = await GetUserRolePermissionReportAsync();
            using var memoryStream = new MemoryStream();
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            document.Add(new Paragraph("Reporte de Roles y Permisos de Usuarios")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                .SetFontSize(20)
                .SetBold());

            Table table = new Table(4, true);
            table.AddHeaderCell("Nombre de Usuario");
            table.AddHeaderCell("Rol");
            table.AddHeaderCell("Permiso");
            table.AddHeaderCell("Fecha de Creación del Permiso");

            foreach (var item in data)
            {
                table.AddCell(item.UserName);
                table.AddCell(item.RoleName);
                table.AddCell(item.PermissionName);
                table.AddCell(item.PermissionCreationDate.ToString("dd/MM/yyyy"));
            }

            document.Add(table);
            document.Close();
            return memoryStream.ToArray();
        }

        public async Task<byte[]> GenerateUserRolePermissionCsvReportAsync()
        {
            var data = await GetUserRolePermissionReportAsync();
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Nombre de Usuario,Rol,Permiso,Fecha de Creación del Permiso");

            foreach (var item in data)
            {
                csvBuilder.AppendLine($"{item.UserName},{item.RoleName},{item.PermissionName},{item.PermissionCreationDate:dd/MM/yyyy}");
            }

            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }

        public async Task<byte[]> GenerateUserRolePermissionExcelReportAsync()
        {
            var data = await GetUserRolePermissionReportAsync();
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Roles y Permisos de Usuarios");

            worksheet.Cells[1, 1].Value = "Nombre de Usuario";
            worksheet.Cells[1, 2].Value = "Rol";
            worksheet.Cells[1, 3].Value = "Permiso";
            worksheet.Cells[1, 4].Value = "Fecha de Creación del Permiso";

            int row = 2;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = item.UserName;
                worksheet.Cells[row, 2].Value = item.RoleName;
                worksheet.Cells[row, 3].Value = item.PermissionName;
                worksheet.Cells[row, 4].Value = item.PermissionCreationDate.ToString("dd/MM/yyyy");
                row++;
            }

            return package.GetAsByteArray();
        }
    }
}
