using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asas.Models;

namespace Asas.Data
{
    public partial class AsasContext : DbContext
    {
        public AsasContext()
        {
        }

        public AsasContext(DbContextOptions<AsasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acc> Acc { get; set; }

        public virtual DbSet<AddE> AddE { get; set; }

        public virtual DbSet<Company> Company { get; set; }

        public virtual DbSet<Dep> Dep { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=HP\\SQLEXPRESS;Initial Catalog=SKY;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>()
        .Property(i => i.Id)
        .ValueGeneratedOnAdd();
            modelBuilder.Entity<Acc>(entity =>
            {
                entity.HasKey(e => new { e.ComId, e.EmpId }).HasName("PK__Acc__FA34C6392624548B");

                entity.ToTable("Acc");

                entity.Property(e => e.ComId).HasColumnName("Com_id");
                entity.Property(e => e.EmpId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Emp_id");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Pass)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneN)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.AddE).WithOne(p => p.Acc)
                    .HasForeignKey<Acc>(d => new { d.ComId, d.EmpId })
                    .HasConstraintName("A");
            });

            modelBuilder.Entity<AddE>(entity =>
            {
                entity.HasKey(e => new { e.ComId, e.EmpId }).HasName("PK__AddE__FA34C6393A67D366");

                entity.ToTable("AddE");

                entity.Property(e => e.ComId).HasColumnName("Com_id");
                entity.Property(e => e.EmpId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Emp_id");
                entity.Property(e => e.DepId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Dep_id");
                entity.Property(e => e.Role)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dep).WithMany(p => p.AddEs)
                    .HasForeignKey(d => new { d.ComId, d.DepId })
                    .HasConstraintName("E");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.ComId).HasName("PK__Company__D85724E45D221B33");

                entity.ToTable("Company");

                entity.Property(e => e.ComId)
                    .ValueGeneratedNever()
                    .HasColumnName("Com_id");
                entity.Property(e => e.DateOfEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_of_end");
                entity.Property(e => e.EmpId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Emp_id");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<Dep>(entity =>
            {
                entity.HasKey(e => new { e.ComId, e.DepId }).HasName("PK__Dep__9895A0F1FF9E6E83");

                entity.ToTable("Dep");

                entity.Property(e => e.ComId).HasColumnName("Com_id");
                entity.Property(e => e.DepId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Dep_id");
                entity.Property(e => e.DepN)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Dep_N");
                entity.Property(e => e.EmpId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("Emp_id");

                entity.HasMany(d => d.AddEs).WithOne(p => p.Dep)
                    .HasForeignKey(d => new { d.ComId, d.EmpId })
                    .HasConstraintName("FK_Dep_to_AddE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}