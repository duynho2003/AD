using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab01.Models;

public partial class AdddbContext : DbContext
{
    public AdddbContext()
    {
    }

    public AdddbContext(DbContextOptions<AdddbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("server=LAPTOP-PH1AFEK8\\SQLEXPRESS;database=ADDB;uid=sa;pwd=123;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Subject).HasName("PK__Marks__A2B1D90577065E15");

            entity.Property(e => e.Subject)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Mark1)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("Mark");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.StudentCodeNavigation).WithMany(p => p.Marks)
                .HasForeignKey(d => d.StudentCode)
                .HasConstraintName("FK__Marks__Mark__4BAC3F29");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentCode).HasName("PK__Students__1FC88605B40A8EB4");

            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
