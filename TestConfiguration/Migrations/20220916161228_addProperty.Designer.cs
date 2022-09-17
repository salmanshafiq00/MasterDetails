﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestConfiguration.DAL;

namespace TestConfiguration.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220916161228_addProperty")]
    partial class addProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TestConfiguration.Models.TestConfig", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AdmissionReferenceId")
                        .HasColumnType("int");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<decimal>("MinimumGPA")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PassMark")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProfileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SesionId")
                        .HasColumnType("int");

                    b.Property<string>("SignatureUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TestConfigs");
                });

            modelBuilder.Entity("TestConfiguration.Models.TestConfigDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ResultGradeId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("TestConfigId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TestConfigId");

                    b.ToTable("TestConfigDetails");
                });

            modelBuilder.Entity("TestConfiguration.Models.TestConfigDetail", b =>
                {
                    b.HasOne("TestConfiguration.Models.TestConfig", null)
                        .WithMany("TestConfigDetails")
                        .HasForeignKey("TestConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestConfiguration.Models.TestConfig", b =>
                {
                    b.Navigation("TestConfigDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
