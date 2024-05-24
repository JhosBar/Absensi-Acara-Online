using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Absensi.EF.Data;

public partial class AbsensiContext : DbContext
{
    public AbsensiContext()
    {
    }

    public AbsensiContext(DbContextOptions<AbsensiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MtAdmin> MtAdmins { get; set; }

    public virtual DbSet<MtEvent> MtEvents { get; set; }

    public virtual DbSet<MtPermission> MtPermissions { get; set; }

    public virtual DbSet<MtRole> MtRoles { get; set; }

    public virtual DbSet<PRecap> PRecaps { get; set; }

    public virtual DbSet<PRolePermission> PRolePermissions { get; set; }

    public virtual DbSet<PTransaction> PTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DSG01;User ID=sa;Password=sasa;Database=absensidb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MtAdmin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtAdmin__3213E83F30D4F28A");

            entity.ToTable("mtAdmin");

            entity.HasIndex(e => e.Username, "UC_mtAdmin").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(200)
                .HasColumnName("password_salt");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .HasColumnName("status");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(200)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.MtAdmins).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<MtEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtBrand__3213E83F38CA7435");

            entity.ToTable("mtEvent");

            entity.HasIndex(e => e.Name, "UC_mtBrand").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.EDate).HasColumnName("eDate");
            entity.Property(e => e.EHour)
                .HasColumnType("datetime")
                .HasColumnName("eHour");
            entity.Property(e => e.ELocation)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("eLocation");
            entity.Property(e => e.ENote)
                .IsUnicode(false)
                .HasColumnName("eNote");
            entity.Property(e => e.FlgDeleted).HasColumnName("flg_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");
        });

        modelBuilder.Entity<MtPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtPermis__3213E83F8D35AEF3");

            entity.ToTable("mtPermission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Display)
                .HasMaxLength(200)
                .HasColumnName("display");
            entity.Property(e => e.Seq).HasColumnName("seq");
            entity.Property(e => e.SubSeq).HasColumnName("sub_seq");
        });

        modelBuilder.Entity<MtRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mtRole__3213E83F775E1813");

            entity.ToTable("mtRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Role)
                .HasMaxLength(200)
                .HasColumnName("role");
        });

        modelBuilder.Entity<PRecap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pRecap__3213E83FE28378FC");

            entity.ToTable("pRecap");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ASignature)
                .HasMaxLength(255)
                .HasColumnName("aSignature");
            entity.Property(e => e.AttenderEmail)
                .HasMaxLength(30)
                .HasColumnName("attenderEmail");
            entity.Property(e => e.AttenderName)
                .HasMaxLength(128)
                .HasColumnName("attenderName");
            entity.Property(e => e.AttenderPhone)
                .HasMaxLength(15)
                .HasColumnName("attenderPhone");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.FlgDeleted).HasColumnName("flg_deleted");

            entity.HasOne(d => d.Event).WithMany(p => p.PRecaps)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK_pRecap_mtEvent_eventId");
        });

        modelBuilder.Entity<PRolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pRolePer__3213E83FE8C2E55F");

            entity.ToTable("pRolePermission");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Permission).WithMany(p => p.PRolePermissions).HasForeignKey(d => d.PermissionId);

            entity.HasOne(d => d.Role).WithMany(p => p.PRolePermissions).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<PTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pTransac__3213E83F7B342D01");

            entity.ToTable("pTransaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnalyzeData)
                .HasMaxLength(500)
                .HasColumnName("analyze_data");
            entity.Property(e => e.AnalyzeTitle)
                .HasMaxLength(255)
                .HasColumnName("analyze_title");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(128)
                .HasColumnName("created_by");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("end_time");
            entity.Property(e => e.FlgDeleted).HasColumnName("flg_deleted");
            entity.Property(e => e.PrimaryData)
                .HasMaxLength(500)
                .HasColumnName("primary_data");
            entity.Property(e => e.PrimaryTitle)
                .HasMaxLength(255)
                .HasColumnName("primary_title");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .HasColumnName("result");
            entity.Property(e => e.SecondaryData)
                .HasMaxLength(500)
                .HasColumnName("secondary_data");
            entity.Property(e => e.SecondaryTitle)
                .HasMaxLength(255)
                .HasColumnName("secondary_title");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");
            entity.Property(e => e.Updated)
                .HasColumnType("datetime")
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(128)
                .HasColumnName("updated_by");

            entity.HasOne(d => d.Brand).WithMany(p => p.PTransactions)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK_pTransaction_mtBrand_brandId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
