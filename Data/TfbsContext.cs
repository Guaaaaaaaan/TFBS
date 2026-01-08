using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TFBS.Data.Entities;

namespace TFBS.Data;

public partial class TfbsContext : DbContext
{
    public TfbsContext()
    {
    }

    public TfbsContext(DbContextOptions<TfbsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<MaintenanceDetail> MaintenanceDetails { get; set; }

    public virtual DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    public virtual DbSet<Mechanic> Mechanics { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<PartUsage> PartUsages { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__0148818EE4D48010");

            entity.ToTable("Department", "tfbs");

            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.DeptName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__Faculty__306F636E83C8D15D");

            entity.ToTable("Faculty", "tfbs");

            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Faculty_Department");
        });

        modelBuilder.Entity<MaintenanceDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__Maintena__135C314D158ABB5B");

            entity.ToTable("MaintenanceDetail", "tfbs");

            entity.Property(e => e.DetailId).HasColumnName("DetailID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.PerformedByMechanicId).HasColumnName("PerformedByMechanicID");

            entity.HasOne(d => d.Log).WithMany(p => p.MaintenanceDetails)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("FK_MDetail_Log");

            entity.HasOne(d => d.PerformedByMechanic).WithMany(p => p.MaintenanceDetails)
                .HasForeignKey(d => d.PerformedByMechanicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MDetail_PerformedByMechanic");
        });

        modelBuilder.Entity<MaintenanceLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__Maintena__5E5499A80293DBD4");

            entity.ToTable("MaintenanceLog", "tfbs");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.ReleasedByMechanicId).HasColumnName("ReleasedByMechanicID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.ReleasedByMechanic).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.ReleasedByMechanicId)
                .HasConstraintName("FK_MLog_ReleasedByMechanic");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.MaintenanceLogs)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MLog_Vehicle");
        });

        modelBuilder.Entity<Mechanic>(entity =>
        {
            entity.HasKey(e => e.MechanicId).HasName("PK__Mechanic__6B040DD19D533CF1");

            entity.ToTable("Mechanic", "tfbs");

            entity.Property(e => e.MechanicId).HasColumnName("MechanicID");
            entity.Property(e => e.MechanicName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__Part__7C3F0D3029C26A3F");

            entity.ToTable("Part", "tfbs");

            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.PartName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PartUsage>(entity =>
        {
            entity.HasKey(e => e.UsageId).HasName("PK__PartUsag__29B197C02950D4E7");

            entity.ToTable("PartUsage", "tfbs");

            entity.HasIndex(e => new { e.LogId, e.PartId }, "UQ_PartUsage_LogPart").IsUnique();

            entity.Property(e => e.UsageId).HasColumnName("UsageID");
            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.PartId).HasColumnName("PartID");

            entity.HasOne(d => d.Log).WithMany(p => p.PartUsages)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("FK_PartUsage_Log");

            entity.HasOne(d => d.Part).WithMany(p => p.PartUsages)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartUsage_Part");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F04B68CC958");

            entity.ToTable("Reservation", "tfbs");

            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.DeptId).HasColumnName("DeptID");
            entity.Property(e => e.Destination)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.RequiredTypeId).HasColumnName("RequiredTypeID");

            entity.HasOne(d => d.Dept).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Department");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_Faculty");

            entity.HasOne(d => d.RequiredType).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.RequiredTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservation_RequiredType");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trip__51DC711EACD74B5B");

            entity.ToTable("Trip", "tfbs");

            entity.HasIndex(e => e.ReservationId, "UQ__Trip__B7EE5F052AA487E0").IsUnique();

            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.CreditCardNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.FuelGallons).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.MaintenanceComplaint)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReservationId).HasColumnName("ReservationID");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Trips)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_Faculty");

            entity.HasOne(d => d.Reservation).WithOne(p => p.Trip)
                .HasForeignKey<Trip>(d => d.ReservationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_Reservation");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Trips)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trip_Vehicle");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicle__476B54B207475FE8");

            entity.ToTable("Vehicle", "tfbs");

            entity.HasIndex(e => e.PlateNumber, "UQ__Vehicle__03692624AB197A4C").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Type).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vehicle_VehicleType");
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__VehicleT__516F03958B168D5A");

            entity.ToTable("VehicleType", "tfbs");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.MileageRate).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.TypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
