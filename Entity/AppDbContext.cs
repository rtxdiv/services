using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace services.Entity;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("requests");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminNoti)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("admin_noti");
            entity.Property(e => e.Contact)
                .HasColumnType("text")
                .HasColumnName("contact");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Query)
                .HasColumnType("text")
                .HasColumnName("query");
            entity.Property(e => e.ServiceName)
                .HasColumnType("text")
                .HasColumnName("service_name");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UserId)
                .HasColumnType("text")
                .HasColumnName("user_id");
            entity.Property(e => e.UserNoti).HasColumnName("user_noti");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
            entity.Property(e => e.Requirements)
                .HasColumnType("text")
                .HasColumnName("requirements");
            entity.Property(e => e.Visible)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("visible");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
