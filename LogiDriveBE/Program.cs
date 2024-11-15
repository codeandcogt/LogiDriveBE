using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL;
using LogiDriveBE.DAL.LogiDriveContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text;
using LogiDriveBE.AUTH.Aao;
using LogiDriveBE.AUTH.Services;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Services;
using LogiDriveBE.BAL.Services;
using LogiDriveBE.BAL.Bao;
using System.Text.Json.Serialization;
using LogiDriveBE.SAL.Sao;
using LogiDriveBE.SAL.Services;
using Amazon;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime; // Asegúrate de que este using apunte al namespace correcto de tu DbContext

var builder = WebApplication.CreateBuilder(args);

// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
        };
    });



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("SuperAdminOrAdmin", policy => policy.RequireRole("SuperAdmin", "Admin"));
});

builder.Services.AddAWSService<IAmazonS3>(new AWSOptions
{
    Credentials = new BasicAWSCredentials(
        builder.Configuration["AWS:AccessKey"],
        builder.Configuration["AWS:SecretKey"]
    ),
    Region = RegionEndpoint.GetBySystemName(builder.Configuration["AWS:Region"])
});

builder.Services.AddScoped<IS3Service, S3Service>();

// Register services
builder.Services.AddScoped<IAuthAao, AuthAao>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Register DAL services
builder.Services.AddScoped<IAreaDao, AreaDaoService>();
builder.Services.AddScoped<ICollaboratorDao, CollaboratorDaoService>();
builder.Services.AddScoped<IAppUserDao, AppUserDaoService>();
builder.Services.AddScoped<IRoleDao, RoleDaoService>();
builder.Services.AddScoped<IPermissionDao, PermissionDaoService>();
builder.Services.AddScoped<IDepartmentDao, DepartmentDaoService>();
builder.Services.AddScoped<ITownDao, TownDaoService>();
builder.Services.AddScoped<ILogProcessDao, LogProcessDaoService>();
builder.Services.AddScoped<ILogReservationDao, LogReservationDaoService>();
builder.Services.AddScoped<IVehicleAssignmentDao, VehicleAssignmentDaoService>();
builder.Services.AddScoped<IMaintenancePartDao, MaintenancePartDaoService>();
builder.Services.AddScoped<IServiceDao, ServiceDaoService>();
builder.Services.AddScoped<IVehicleDao, VehicleDaoService>();
builder.Services.AddScoped<IPartVehicleDao, PartVehicleDaoService>();
builder.Services.AddScoped<IPreliminaryInspectionSheetDao, PreliminaryInspectionSheetDaoService>();
builder.Services.AddScoped<ILogInspectionDao, LogInspectionDaoService>();
builder.Services.AddScoped<ILogInspectionPartDao, LogInspectionPartDaoService>();
builder.Services.AddScoped<IVehicleAssignmentReportDao, VehicleAssignmentReportDaoService>();
builder.Services.AddScoped<IVehicleInspectionReportDao, VehicleInspectionReportDaoService>();
builder.Services.AddScoped<ILogTrackingDao, LogTrackingDao>();
builder.Services.AddScoped<ILogTripDao, LogTripDao>();
builder.Services.AddScoped<IProcessLogReportDao, ProcessLogReportDaoService>();
builder.Services.AddScoped<IReportDao, ReportDaoService>();
builder.Services.AddScoped<IVehicleProcessReservationReportDao, VehicleProcessReservationReportDaoService>();
builder.Services.AddScoped<IUserRolePermissionReportDao, UserRolePermissionReportDaoService>();
builder.Services.AddScoped<IActivityByCollaboratorReportDao, ActivityByCollaboratorReportDaoService>();
builder.Services.AddScoped<ILogTripReportDao, LogTripReportDaoService>();
builder.Services.AddScoped<IVehicleAvailabilityReportDao, VehicleAvailabilityReportDaoService>();
builder.Services.AddScoped<IMaintenanceReportDao, MaintenanceReportDaoService>();
// Register BAL services
builder.Services.AddScoped<IAreaBao, AreaBaoService>();
builder.Services.AddScoped<ICollaboratorBao, CollaboratorBaoService>();
builder.Services.AddScoped<IAppUserBao, AppUserBaoService>();
builder.Services.AddScoped<IRoleBao, RoleBaoService>();
builder.Services.AddScoped<IPermissionBao, PermissionBaoService>();
builder.Services.AddScoped<IDepartmentBao, DepartmentBaoService>();
builder.Services.AddScoped<ITownBao, TownBaoService>();
builder.Services.AddScoped<ILogProcessBao, LogProcessBaoService>();
builder.Services.AddScoped<ILogReservationBao, LogReservationBaoService>();
builder.Services.AddScoped<IVehicleAssignmentBao, VehicleAssignmentBaoService>();
builder.Services.AddScoped<IMaintenancePartBao, MaintenancePartBaoService>();
builder.Services.AddScoped<IServiceBao,  ServiceBaoService>();
builder.Services.AddScoped<IVehicleBao, VehicleBaoService>();
builder.Services.AddScoped<IPartVehicleBao, PartVehicleBaoService>();
builder.Services.AddScoped<IVehicleInspectionReportBao, VehicleInspectionReportBaoService>();
builder.Services.AddScoped<IProcessLogReportBao, ProcessLogReportBaoService>();
builder.Services.AddScoped<ILogTripReportBao, LogTripReportBaoService>();
builder.Services.AddScoped<IVehicleAssignmentReportBao, VehicleAssignmentReportBaoService>();
builder.Services.AddScoped<IVehicleAvailabilityReportBao, VehicleAvailabilityReportBaoService>();
builder.Services.AddScoped<IPreliminaryInspectionSheetBao, PreliminaryInspectionSheetBaoService>();
builder.Services.AddScoped<IActivityByCollaboratorReportBao, ActivityByCollaboratorReportBaoService>();
builder.Services.AddScoped<ILogTrackingBao, LogTrackingBaoService>();
builder.Services.AddScoped<ILogTripBao, LogTripBaoService>();
builder.Services.AddScoped<ILogInspectionBao, LogInspectionBaoService>();
builder.Services.AddScoped<ILogInspectionPartBao, LogInspectionPartBaoService>();
builder.Services.AddScoped<IReportBao, ReportBaoService>();
builder.Services.AddScoped<IUserRolePermissionReportBao, UserRolePermissionReportBaoService>();
builder.Services.AddScoped<IVehicleProcessReservationReportBao, VehicleProcessReservationReportBaoService>();
builder.Services.AddScoped<IMaintenanceReportBao, MaintenanceReportBaoService>();
// Add services to the container.
builder.Services.AddControllers();
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Añade esta línea para configurar el DbContext
builder.Services.AddDbContext<LogiDriveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins(allowedOrigins)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Usa CORS
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();