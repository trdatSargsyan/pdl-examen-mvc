﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.AppDbContext;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221209140340_CreditCartMigration")]
    partial class CreditCartMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApi.Models.Aeroport", b =>
                {
                    b.Property<int>("AeroportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AeroportId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AeroportId");

                    b.HasIndex("CountryId");

                    b.ToTable("Aeroports");
                });

            modelBuilder.Entity("WebApi.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DistanceTraveled")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.Property<double>("Solde")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("WebApi.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AeroportId")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GearboxId")
                        .HasColumnType("int");

                    b.Property<double>("Km")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MotorId")
                        .HasColumnType("int");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Seater")
                        .HasColumnType("int");

                    b.Property<int?>("TypeOfCarId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AeroportId");

                    b.HasIndex("GearboxId");

                    b.HasIndex("MotorId");

                    b.HasIndex("TypeOfCarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("WebApi.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("WebApi.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WebApi.Models.CreditCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateValid")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("WebApi.Models.FormulePrice", b =>
                {
                    b.Property<int>("FormulePriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FormulePriceId"), 1L, 1);

                    b.Property<int?>("AeroportId")
                        .HasColumnType("int");

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<double>("PriceDay")
                        .HasColumnType("float");

                    b.Property<double>("PriceWeek")
                        .HasColumnType("float");

                    b.HasKey("FormulePriceId");

                    b.HasIndex("AeroportId");

                    b.HasIndex("CarId")
                        .IsUnique();

                    b.ToTable("FormulePrices");
                });

            modelBuilder.Entity("WebApi.Models.Gearbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gearboxes");
                });

            modelBuilder.Entity("WebApi.Models.Motor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Motors");
                });

            modelBuilder.Entity("WebApi.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Start_Km")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("ClientId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WebApi.Models.TypeOfCar", b =>
                {
                    b.Property<int>("TypeOfCarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TypeOfCarId"), 1L, 1);

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeOfCarId");

                    b.ToTable("TypeOfCars");
                });

            modelBuilder.Entity("WebApi.Models.Aeroport", b =>
                {
                    b.HasOne("WebApi.Models.Country", "Country")
                        .WithMany("Aeroports")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("WebApi.Models.Bill", b =>
                {
                    b.HasOne("WebApi.Models.Reservation", "Reservation")
                        .WithOne("Bill")
                        .HasForeignKey("WebApi.Models.Bill", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("WebApi.Models.Car", b =>
                {
                    b.HasOne("WebApi.Models.Aeroport", "Aeroport")
                        .WithMany("Cars")
                        .HasForeignKey("AeroportId");

                    b.HasOne("WebApi.Models.Gearbox", "Gearbox")
                        .WithMany("Cars")
                        .HasForeignKey("GearboxId");

                    b.HasOne("WebApi.Models.Motor", "Motor")
                        .WithMany("Cars")
                        .HasForeignKey("MotorId");

                    b.HasOne("WebApi.Models.TypeOfCar", "TypeOfCar")
                        .WithMany("Cars")
                        .HasForeignKey("TypeOfCarId");

                    b.Navigation("Aeroport");

                    b.Navigation("Gearbox");

                    b.Navigation("Motor");

                    b.Navigation("TypeOfCar");
                });

            modelBuilder.Entity("WebApi.Models.CreditCard", b =>
                {
                    b.HasOne("WebApi.Models.Client", "Client")
                        .WithMany("CreditCards")
                        .HasForeignKey("ClientId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("WebApi.Models.FormulePrice", b =>
                {
                    b.HasOne("WebApi.Models.Aeroport", "Aeroport")
                        .WithMany("Prices")
                        .HasForeignKey("AeroportId");

                    b.HasOne("WebApi.Models.Car", "Car")
                        .WithOne("Price")
                        .HasForeignKey("WebApi.Models.FormulePrice", "CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aeroport");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("WebApi.Models.Reservation", b =>
                {
                    b.HasOne("WebApi.Models.Car", "Car")
                        .WithMany("Reservations")
                        .HasForeignKey("CarId");

                    b.HasOne("WebApi.Models.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("ClientId");

                    b.Navigation("Car");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("WebApi.Models.Aeroport", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Prices");
                });

            modelBuilder.Entity("WebApi.Models.Car", b =>
                {
                    b.Navigation("Price");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("WebApi.Models.Client", b =>
                {
                    b.Navigation("CreditCards");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("WebApi.Models.Country", b =>
                {
                    b.Navigation("Aeroports");
                });

            modelBuilder.Entity("WebApi.Models.Gearbox", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("WebApi.Models.Motor", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("WebApi.Models.Reservation", b =>
                {
                    b.Navigation("Bill");
                });

            modelBuilder.Entity("WebApi.Models.TypeOfCar", b =>
                {
                    b.Navigation("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
