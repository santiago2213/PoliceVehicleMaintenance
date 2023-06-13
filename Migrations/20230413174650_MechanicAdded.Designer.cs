﻿// <auto-generated />
using System;
using DiscussionMvcSantiago.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiscussionMvcSantiago.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230413174650_MechanicAdded")]
    partial class MechanicAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DiscussionLibrarySantiago.Notes", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MechanicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("NoteCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ServiceRequestId")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("NoteId");

                    b.HasIndex("MechanicId");

                    b.HasIndex("ServiceRequestId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.ServiceRequest", b =>
                {
                    b.Property<int>("ServiceRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceRequestId"), 1L, 1);

                    b.Property<DateTime>("DateServiceRequested")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateServiced")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateSupervisorDecision")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MechanicId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("OfficerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ServiceRequestAddedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SupervisorDecision")
                        .HasColumnType("int");

                    b.Property<string>("SupervisorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("SupervisorOfficerId")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("ServiceRequestId");

                    b.HasAlternateKey("VehicleId", "DateServiceRequested");

                    b.HasIndex("MechanicId");

                    b.HasIndex("OfficerId");

                    b.HasIndex("SupervisorId");

                    b.HasIndex("SupervisorOfficerId");

                    b.ToTable("ServiceRequest");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.SupervisorOfficer", b =>
                {
                    b.Property<int>("SupervisorOfficerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupervisorOfficerId"), 1L, 1);

                    b.Property<DateTime?>("DateSupervisionEnded")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateSupervisionStarted")
                        .HasColumnType("datetime2");

                    b.Property<string>("OfficerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SupervisorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SupervisorOfficerId");

                    b.HasIndex("OfficerId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("SupervisorOfficer");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VehicleAddedBySupervisorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("VehicleAddedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VehicleMakeId")
                        .HasColumnType("int");

                    b.Property<int?>("VehicleModelId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleStatus")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("VehicleId");

                    b.HasAlternateKey("VIN");

                    b.HasIndex("VehicleAddedBySupervisorId");

                    b.HasIndex("VehicleMakeId");

                    b.HasIndex("VehicleModelId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.VehicleMake", b =>
                {
                    b.Property<int>("VehicleMakeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleMakeId"), 1L, 1);

                    b.Property<string>("VehicleMakeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleMakeId");

                    b.ToTable("VehicleMake");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.VehicleModel", b =>
                {
                    b.Property<int>("VehicleModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleModelId"), 1L, 1);

                    b.Property<int>("VehicleMakeId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleModelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VehicleModelId");

                    b.HasIndex("VehicleMakeId");

                    b.ToTable("VehicleModel");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Mechanic", b =>
                {
                    b.HasBaseType("DiscussionLibrarySantiago.AppUser");

                    b.HasDiscriminator().HasValue("Mechanic");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Officer", b =>
                {
                    b.HasBaseType("DiscussionLibrarySantiago.AppUser");

                    b.HasDiscriminator().HasValue("Officer");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Supervisor", b =>
                {
                    b.HasBaseType("DiscussionLibrarySantiago.AppUser");

                    b.HasDiscriminator().HasValue("Supervisor");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Notes", b =>
                {
                    b.HasOne("DiscussionLibrarySantiago.Mechanic", "Mechanic")
                        .WithMany("MechanicNotes")
                        .HasForeignKey("MechanicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionLibrarySantiago.ServiceRequest", "ServiceRequest")
                        .WithMany("ServiceRequestNotes")
                        .HasForeignKey("ServiceRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionLibrarySantiago.Supervisor", "Supervisor")
                        .WithMany("SupervisorNotes")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mechanic");

                    b.Navigation("ServiceRequest");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.ServiceRequest", b =>
                {
                    b.HasOne("DiscussionLibrarySantiago.Mechanic", "Mechanic")
                        .WithMany("ServiceRequestWorkedOn")
                        .HasForeignKey("MechanicId");

                    b.HasOne("DiscussionLibrarySantiago.Officer", "Officer")
                        .WithMany("ServiceRequestsSubmitted")
                        .HasForeignKey("OfficerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionLibrarySantiago.Supervisor", "Supervisor")
                        .WithMany("ServiceRequestsDecided")
                        .HasForeignKey("SupervisorId");

                    b.HasOne("DiscussionLibrarySantiago.SupervisorOfficer", "ServiceRequestAddedByOfficer")
                        .WithMany()
                        .HasForeignKey("SupervisorOfficerId");

                    b.HasOne("DiscussionLibrarySantiago.Vehicle", "Vehicle")
                        .WithMany("VehicleServiceRequests")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mechanic");

                    b.Navigation("Officer");

                    b.Navigation("ServiceRequestAddedByOfficer");

                    b.Navigation("Supervisor");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.SupervisorOfficer", b =>
                {
                    b.HasOne("DiscussionLibrarySantiago.Officer", "Officer")
                        .WithMany("Supervisors")
                        .HasForeignKey("OfficerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscussionLibrarySantiago.Supervisor", "Supervisor")
                        .WithMany("OfficersSupervised")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Officer");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Vehicle", b =>
                {
                    b.HasOne("DiscussionLibrarySantiago.Supervisor", "VehicleAddedBySupervisor")
                        .WithMany()
                        .HasForeignKey("VehicleAddedBySupervisorId");

                    b.HasOne("DiscussionLibrarySantiago.VehicleMake", "VehicleMake")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleMakeId");

                    b.HasOne("DiscussionLibrarySantiago.VehicleModel", "VehicleModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("VehicleModelId");

                    b.Navigation("VehicleAddedBySupervisor");

                    b.Navigation("VehicleMake");

                    b.Navigation("VehicleModel");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.VehicleModel", b =>
                {
                    b.HasOne("DiscussionLibrarySantiago.VehicleMake", "VehicleMake")
                        .WithMany("VehicleModels")
                        .HasForeignKey("VehicleMakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehicleMake");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.ServiceRequest", b =>
                {
                    b.Navigation("ServiceRequestNotes");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Vehicle", b =>
                {
                    b.Navigation("VehicleServiceRequests");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.VehicleMake", b =>
                {
                    b.Navigation("VehicleModels");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.VehicleModel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Mechanic", b =>
                {
                    b.Navigation("MechanicNotes");

                    b.Navigation("ServiceRequestWorkedOn");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Officer", b =>
                {
                    b.Navigation("ServiceRequestsSubmitted");

                    b.Navigation("Supervisors");
                });

            modelBuilder.Entity("DiscussionLibrarySantiago.Supervisor", b =>
                {
                    b.Navigation("OfficersSupervised");

                    b.Navigation("ServiceRequestsDecided");

                    b.Navigation("SupervisorNotes");
                });
#pragma warning restore 612, 618
        }
    }
}
