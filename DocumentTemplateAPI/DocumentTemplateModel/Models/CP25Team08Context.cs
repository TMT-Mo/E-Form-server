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

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentXfdfString> DocumentXfdfString { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Template> Template { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserTemplate> UserTemplate { get; set; }
        public virtual DbSet<XfdfString> XfdfString { get; set; }

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
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TypesName)
                    .HasColumnName("types_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasColumnName("document_name")
                    .HasMaxLength(500);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsLocked).HasColumnName("isLocked");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Reason).HasColumnName("reason");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.TypesName)
                    .HasColumnName("types_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");

                entity.Property(e => e.Version)
                    .HasColumnName("version")
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_document_user");
            });

            modelBuilder.Entity<DocumentXfdfString>(entity =>
            {
                entity.ToTable("document_xfdfString");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IdDocument).HasColumnName("id_document");

                entity.Property(e => e.IdXfdfString).HasColumnName("id_xfdfString");

                entity.Property(e => e.Queue).HasColumnName("queue");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany(p => p.DocumentXfdfString)
                    .HasForeignKey(d => d.IdDocument)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_document_xfdfString_document");

                entity.HasOne(d => d.IdXfdfStringNavigation)
                    .WithMany(p => p.DocumentXfdfString)
                    .HasForeignKey(d => d.IdXfdfString)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_document_xfdfString_xfdfString");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("template");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IdType).HasColumnName("id_type");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.IsEnable)
                    .HasColumnName("isEnable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Link).HasColumnName("link");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TemplateName)
                    .IsRequired()
                    .HasColumnName("template_name")
                    .HasMaxLength(500);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");

                entity.HasOne(d => d.IdTypeNavigation)
                    .WithMany(p => p.Template)
                    .HasForeignKey(d => d.IdType)
                    .HasConstraintName("FK_template_category");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(100);

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(100);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20);

                entity.Property(e => e.Signature).HasColumnName("signature");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");

                entity.Property(e => e.Username)
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.IdDepartment).HasColumnName("id_department");

                entity.Property(e => e.IdRole).HasColumnName("id_role");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdDepartment)
                    .HasConstraintName("FK_user_role_department");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_role");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_user_role_user");
            });

            modelBuilder.Entity<UserTemplate>(entity =>
            {
                entity.ToTable("user_template");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTemplate).HasColumnName("id_template");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.IdTemplateNavigation)
                    .WithMany(p => p.UserTemplate)
                    .HasForeignKey(d => d.IdTemplate)
                    .HasConstraintName("FK_user_template_template");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserTemplate)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_user_template_user");
            });

            modelBuilder.Entity<XfdfString>(entity =>
            {
                entity.ToTable("xfdfString");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updateAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.UpdateBy).HasColumnName("updateBy");

                entity.Property(e => e.XfdfString1)
                    .IsRequired()
                    .HasColumnName("xfdfString");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
