using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DocumentTemplateModel.Models
{
    public partial class CP25Team08Context : DbContext
    {
        public CP25Team08Context()
        {
        }

        public CP25Team08Context(DbContextOptions<CP25Team08Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tuleap.vanlanguni.edu.vn,18082;User Id=CP25Team08;Password=CP25Team08;Database=CP25Team08;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.DocumentName)
                    .HasColumnName("document_name")
                    .HasMaxLength(500);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(100);

                entity.Property(e => e.StatusDocument).HasColumnName("status_document");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_document_user");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("template");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.Size)
                    .HasColumnName("size")
                    .HasMaxLength(100);

                entity.Property(e => e.StatusTemplate).HasColumnName("status_template");

                entity.Property(e => e.TemplateName)
                    .HasColumnName("template_name")
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_template_department");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(200);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(200);

                entity.Property(e => e.StatusAccount).HasColumnName("status_account");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_department");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_role__id_ro__3A81B327");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__user_role__id_us__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
