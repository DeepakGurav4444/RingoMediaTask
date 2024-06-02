﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RingoMediaTask.Data;

#nullable disable

namespace RingoMediaTask.Migrations
{
    [DbContext(typeof(AdminDbContext))]
    partial class AdminDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Department", b =>
                {
                    b.Property<int>("IdDepartment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdDepartment");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Management", b =>
                {
                    b.Property<int>("IdManagement")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdManagement");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Managements");
                });

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Operational", b =>
                {
                    b.Property<int>("IdOperational")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("Logo")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<int>("ManagementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdOperational");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ManagementId");

                    b.ToTable("Operationals");
                });

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Reminder", b =>
                {
                    b.Property<int>("IdReminder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EmailForReminder")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte>("IsProcessing")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("ReminderDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReminderFor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("IdReminder");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Management", b =>
                {
                    b.HasOne("RingoMediaTask.Models.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("RingoMediaTask.Models.Entities.Operational", b =>
                {
                    b.HasOne("RingoMediaTask.Models.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RingoMediaTask.Models.Entities.Management", "Management")
                        .WithMany()
                        .HasForeignKey("ManagementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Management");
                });
#pragma warning restore 612, 618
        }
    }
}
