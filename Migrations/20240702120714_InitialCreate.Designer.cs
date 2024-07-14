﻿// <auto-generated />
using System;
using Blackrock_Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blackrock_Test.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240702120714_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Blackrock_Test.Modals.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CollateralValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("OutstandingAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PortfolioId")
                        .HasColumnType("int");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Blackrock_Test.Modals.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Blackrock_Test.Modals.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CreditRating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProbabilityOfDefault")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Blackrock_Test.Modals.RunRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ExpectedLoss")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PercentageChange")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("RunDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ScenarioCollateralValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalCollateralValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalOutstandingLoanAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("RunRecords");
                });

            modelBuilder.Entity("Blackrock_Test.Modals.Loan", b =>
                {
                    b.HasOne("Blackrock_Test.Modals.Portfolio", "Portfolio")
                        .WithMany("Loans")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Blackrock_Test.Modals.Portfolio", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
