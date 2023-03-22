﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsMarkWithExcel.Models;

#nullable disable

namespace StudentsMarkWithExcel.Migrations
{
    [DbContext(typeof(StudentsMarkLiveContext))]
    [Migration("20230321072619_ThirdMigration")]
    partial class ThirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("Mark")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.GroupStudent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("StudentId");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("StudentName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Degree", b =>
                {
                    b.HasOne("StudentsMarkWithExcel.Models.Course", "Course")
                        .WithMany("Degrees")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Degrees_Courses");

                    b.HasOne("StudentsMarkWithExcel.Models.Student", "Student")
                        .WithMany("Degrees")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Degrees_Students");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.GroupStudent", b =>
                {
                    b.HasOne("StudentsMarkWithExcel.Models.Group", "Group")
                        .WithMany("GroupStudent")
                        .HasForeignKey("GroupId");

                    b.HasOne("StudentsMarkWithExcel.Models.Student", "Student")
                        .WithMany("GroupStudent")
                        .HasForeignKey("StudentId");

                    b.Navigation("Group");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Course", b =>
                {
                    b.Navigation("Degrees");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Group", b =>
                {
                    b.Navigation("GroupStudent");
                });

            modelBuilder.Entity("StudentsMarkWithExcel.Models.Student", b =>
                {
                    b.Navigation("Degrees");

                    b.Navigation("GroupStudent");
                });
#pragma warning restore 612, 618
        }
    }
}
