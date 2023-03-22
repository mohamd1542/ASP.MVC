using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentsMarkWithExcel.Models;

namespace StudentsMarkWithExcel.Models;

public partial class StudentsMarkLiveContext : DbContext
{
    public StudentsMarkLiveContext()
    {
    }

    public StudentsMarkLiveContext(DbContextOptions<StudentsMarkLiveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-73IK7RU\\SQLEXPRESS;Initial Catalog=StudentsMarkLive;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseName).HasMaxLength(50);
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasOne(d => d.Course).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Degrees_Courses");

            entity.HasOne(d => d.Student).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_Degrees_Students");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.StudentName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<StudentsMarkWithExcel.Models.Group>? Group { get; set; }

    public DbSet<StudentsMarkWithExcel.Models.GroupStudent>? GroupStudent { get; set; }
}
