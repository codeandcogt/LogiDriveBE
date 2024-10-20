using Microsoft.EntityFrameworkCore;
using LogiDriveBE.DAL;
using LogiDriveBE.DAL.LogiDriveContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LogiDriveBE.AUTH.Aao;
using LogiDriveBE.AUTH.Services;
using LogiDriveBE.DAL.Dao;
using LogiDriveBE.DAL.Services;
using LogiDriveBE.BAL.Services;
using LogiDriveBE.BAL.Bao;
using System.Text.Json.Serialization; // Asegúrate de que este using apunte al namespace correcto de tu DbContext

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