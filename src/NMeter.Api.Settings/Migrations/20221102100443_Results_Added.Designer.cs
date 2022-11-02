﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NMeter.Api.Settings.Data;

#nullable disable

namespace NMeter.Api.Settings.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221102100443_Results_Added")]
    partial class Results_Added
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NMeter.Api.Settings.Models.Execution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Executions");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Header", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.ToTable("Headers");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BaseUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.PlanVariable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("PlanVariables");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.Property<int>("UsersNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanId")
                        .IsUnique();

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ExecutionId")
                        .HasColumnType("int");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResponseCode")
                        .HasColumnType("int");

                    b.Property<string>("ResponseHeaders")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExecutionId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Method")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.UrlParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.ToTable("UrlParameters");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Execution", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Plan", "Plan")
                        .WithMany("Executions")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Header", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Step", "Step")
                        .WithMany("Headers")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Step");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.PlanVariable", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Plan", "Plan")
                        .WithMany("Variables")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Profile", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Plan", "Plan")
                        .WithOne("Profile")
                        .HasForeignKey("NMeter.Api.Settings.Models.Profile", "PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Result", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Execution", "Execution")
                        .WithMany("Results")
                        .HasForeignKey("ExecutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Execution");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Step", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Plan", "Plan")
                        .WithMany("Steps")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.UrlParameter", b =>
                {
                    b.HasOne("NMeter.Api.Settings.Models.Step", "Step")
                        .WithMany("Parameters")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Step");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Execution", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Plan", b =>
                {
                    b.Navigation("Executions");

                    b.Navigation("Profile");

                    b.Navigation("Steps");

                    b.Navigation("Variables");
                });

            modelBuilder.Entity("NMeter.Api.Settings.Models.Step", b =>
                {
                    b.Navigation("Headers");

                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}