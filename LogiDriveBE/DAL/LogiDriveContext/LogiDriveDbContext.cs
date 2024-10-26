using System;
using System.Collections.Generic;
using LogiDriveBE.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace LogiDriveBE.DAL.LogiDriveContext;

public partial class LogiDriveDbContext : DbContext
{
    public LogiDriveDbContext()
    {
    }

    public LogiDriveDbContext(DbContextOptions<LogiDriveDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Collaborator> Collaborators { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<LogInspection> LogInspections { get; set; }

    public virtual DbSet<LogInspectionPart> LogInspectionParts { get; set; }

    public virtual DbSet<LogProcess> LogProcesses { get; set; }

    public virtual DbSet<LogReservation> LogReservations { get; set; }

    public virtual DbSet<LogTracking> LogTrackings { get; set; }

    public virtual DbSet<LogTrip> LogTrips { get; set; }

    public virtual DbSet<MaintenancePart> MaintenanceParts { get; set; }

    public virtual DbSet<PartVehicle> PartVehicles { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PreliminaryInspectionSheet> PreliminaryInspectionSheets { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Town> Towns { get; set; }

    public virtual DbSet<TypeService> TypeServices { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleAssignment> VehicleAssignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:logidrive.database.windows.net,1433;Initial Catalog=LogiDriveDB;Persist Security Info=False;User ID=LogiAdmin;Password=w}usc6[_&v;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.IdAppUser).HasName("PK__AppUser__970B8AFB0F5B86B0");

            entity.ToTable("AppUser");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdRoleUser");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__Area__2FC141AA6E1F8F04");

            entity.ToTable("Area");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<Collaborator>(entity =>
        {
            entity.HasKey(e => e.IdCollaborator).HasName("PK__Collabor__FF69F77F73BA6410");

            entity.ToTable("Collaborator");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(75);
            entity.Property(e => e.LicenseNumber).HasMaxLength(25);
            entity.Property(e => e.LicenseType).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.Phone).HasMaxLength(25);
            entity.Property(e => e.Position).HasMaxLength(50);

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Collaborators)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdArea");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Collaborators)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdUser");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.IdDepartment).HasName("PK__Departme__DF1E6E4BD2838F36");

            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(75);
        });

        modelBuilder.Entity<LogInspection>(entity =>
        {
            entity.HasKey(e => e.IdLogInspection).HasName("PK__LogInspe__20F0B03F71742966");

            entity.ToTable("LogInspection");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Fuel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Odometer)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TypeInspection)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCollaboratorNavigation).WithMany(p => p.LogInspections)
                .HasForeignKey(d => d.IdCollaborator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdCollaboratorLogInspection");

            entity.HasOne(d => d.IdLogProcessNavigation).WithMany(p => p.LogInspections)
                .HasForeignKey(d => d.IdLogProcess)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProcess");

            entity.HasOne(d => d.IdVehicleAssignmentNavigation).WithMany(p => p.LogInspections)
                .HasForeignKey(d => d.IdVehicleAssignment)
                .HasConstraintName("FK_IdVehicleAssignmentLogInspection");
        });

        modelBuilder.Entity<LogInspectionPart>(entity =>
        {
            entity.HasKey(e => e.IdLogInspectionPart).HasName("PK_LogInspectionPart");

            entity.ToTable("LogInspectionPart");

            // Configuración de la columna IdLogInspectionPart como autoincremental
            entity.Property(e => e.IdLogInspectionPart)
                .ValueGeneratedOnAdd(); // Indica que el valor se generará automáticamente

            // Otras configuraciones de columnas
            entity.Property(e => e.Comment)
                .HasMaxLength(255);

            entity.Property(e => e.DateInspection)
                .HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.Image)
                .HasMaxLength(150)
                .IsUnicode(false);

            // Configuraciones de relaciones
            entity.HasOne(d => d.IdLogInspectionNavigation).WithMany(p => p.LogInspectionParts)
                .HasForeignKey(d => d.IdLogInspection)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogInspectionLogInspectionPart");

            entity.HasOne(d => d.IdPartVehicleNavigation).WithMany(p => p.LogInspectionParts)
                .HasForeignKey(d => d.IdPartVehicle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartVehicleLogInspectionPart");
        });


        modelBuilder.Entity<LogProcess>(entity =>
        {
            entity.HasKey(e => e.IdLogProcess).HasName("PK__LogProce__D4B8B4A5507F2E9F");

            entity.ToTable("LogProcess");

            entity.Property(e => e.Action).HasMaxLength(50);

            entity.HasOne(d => d.IdCollaboratorNavigation).WithMany(p => p.LogProcesses)
                .HasForeignKey(d => d.IdCollaborator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdCollaboratorProcess");

            entity.HasOne(d => d.IdLogInspectionNavigation).WithMany(p => p.LogProcesses)
                .HasForeignKey(d => d.IdLogInspection)
                .HasConstraintName("FK_IdLogInspection");

            entity.HasOne(d => d.IdLogReservationNavigation).WithMany(p => p.LogProcesses)
                .HasForeignKey(d => d.IdLogReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdLogReservationLogProcess");

            entity.HasOne(d => d.IdVehicleAssignmentNavigation).WithMany(p => p.LogProcesses)
                .HasForeignKey(d => d.IdVehicleAssignment)
                .HasConstraintName("FK_IdVehicleAssignmentLogProcess");
        });

        modelBuilder.Entity<LogReservation>(entity =>
        {
            entity.HasKey(e => e.IdLogReservation).HasName("PK__LogReser__7412576DA931F9E2");

            entity.ToTable("LogReservation");

            entity.Property(e => e.Addres).HasMaxLength(150);
            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Justify).HasMaxLength(255);
            entity.Property(e => e.StatusReservation)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCollaboratorNavigation).WithMany(p => p.LogReservations)
                .HasForeignKey(d => d.IdCollaborator)
                .HasConstraintName("FK_IdCollaboratorReservation");

            entity.HasOne(d => d.IdTownNavigation).WithMany(p => p.LogReservations)
                .HasForeignKey(d => d.IdTown)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdTownReservation");
        });

        modelBuilder.Entity<LogTracking>(entity =>
        {
            entity.HasKey(e => e.IdTracking).HasName("PK__LogTrack__FDC1E65C7A69CE6A");

            entity.ToTable("LogTracking");

            entity.Property(e => e.Latitude).HasColumnType("decimal(15, 11)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(16, 11)");
        });

        modelBuilder.Entity<LogTrip>(entity =>
        {
            entity.HasKey(e => e.IdLogTrip).HasName("PK__LogTrip__8127A48E52A0711D");

            entity.ToTable("LogTrip");

            entity.Property(e => e.ActivityType).HasMaxLength(50);
            entity.Property(e => e.DateHour).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.IdTrackingNavigation).WithMany(p => p.LogTrips)
                .HasForeignKey(d => d.IdTracking)
                .HasConstraintName("FK_IdTrackingLogTrip");

            entity.HasOne(d => d.IdVehicleAssignmentNavigation).WithMany(p => p.LogTrips)
                .HasForeignKey(d => d.IdVehicleAssignment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdVehicleAssigmentLogTrip");
        });

        modelBuilder.Entity<MaintenancePart>(entity =>
        {
            entity.HasKey(e => e.IdMaintenancePart).HasName("PK__Maintena__BF755D4AE9D993E2");

            entity.ToTable("MaintenancePart");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.DateMaintenancePart).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.IdPartVehicleNavigation).WithMany(p => p.MaintenanceParts)
                .HasForeignKey(d => d.IdPartVehicle)
                .HasConstraintName("FK_IdPartVehicle");
        });

        modelBuilder.Entity<PartVehicle>(entity =>
        {
            entity.HasKey(e => e.IdPartVehicle).HasName("PK__PartVehi__FC089B67C7495BDA");

            entity.ToTable("PartVehicle");

            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(75);
            entity.Property(e => e.StatusPart).HasMaxLength(50);

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.PartVehicles)
                .HasForeignKey(d => d.IdVehicle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdVehicle");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.IdPermission).HasName("PK__Permissi__17C26EA25BAEB328");

            entity.ToTable("Permission");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PreliminaryInspectionSheet>(entity =>
        {
            entity.HasKey(e => e.IdPreliminaryInspectionSheet).HasName("PK__Prelimin__377DC8DFD579D0C4");

            entity.ToTable("PreliminaryInspectionSheet");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.DateSheet).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.IdVehicleAssignmentNavigation).WithMany(p => p.PreliminaryInspectionSheets)
                .HasForeignKey(d => d.IdVehicleAssignment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdVehicleAssigmentPreliminarySheet");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Role__B4369054B416B42C");

            entity.ToTable("Role");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.IdPermissions).WithMany(p => p.IdRoles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("IdPermission")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdPermission"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_IdRole"),
                    j =>
                    {
                        j.HasKey("IdRole", "IdPermission").HasName("PK__RolePerm__854AB6BE2F472441");
                        j.ToTable("RolePermission");
                    });
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.IdService).HasName("PK__Service__474DDE00322DAC55");

            entity.ToTable("Service");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.Maintenance)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NextServie)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTypeServiceNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdTypeService)
                .HasConstraintName("FK_IdTypeService");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.Services)
                .HasForeignKey(d => d.IdVehicle)
                .HasConstraintName("FK_IdVehicleService");
        });

        modelBuilder.Entity<Town>(entity =>
        {
            entity.HasKey(e => e.IdTown).HasName("PK__Town__860C0321EA1BB82E");

            entity.ToTable("Town");

            entity.Property(e => e.Name).HasMaxLength(75);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Towns)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdDepartment");
        });

        modelBuilder.Entity<TypeService>(entity =>
        {
            entity.HasKey(e => e.IdTypeService).HasName("PK__TypeServ__E860F0CE9BCD57FB");

            entity.ToTable("TypeService");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.IdVehicle).HasName("PK__Vehicle__64D74CC8732E9165");

            entity.ToTable("Vehicle");

            entity.Property(e => e.Brand).HasMaxLength(75);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Mileage)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Plate).HasMaxLength(15);
            entity.Property(e => e.StatusVehicle).HasMaxLength(20);
            entity.Property(e => e.Tyoe)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Year)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VehicleAssignment>(entity =>
        {
            entity.HasKey(e => e.IdVehicleAssignment).HasName("PK__VehicleA__5EA94DA1AC50F9D7");

            entity.ToTable("VehicleAssignment");

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.CreationDate).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.TripType).HasMaxLength(50);

            entity.HasOne(d => d.IdLogReservationNavigation).WithMany(p => p.VehicleAssignments)
                .HasForeignKey(d => d.IdLogReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdLogReservation");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.VehicleAssignments)
                .HasForeignKey(d => d.IdVehicle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdVehicleVehicleAssigment");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
