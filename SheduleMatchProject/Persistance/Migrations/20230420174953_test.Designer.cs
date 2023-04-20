﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance.Data;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230420174953_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Domain.Addresses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Domain.Models.Domain.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameClassId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameClassId");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("Domain.Models.Domain.FootballPitch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AddressesId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressesId");

                    b.ToTable("FootballPitches");
                });

            modelBuilder.Entity("Domain.Models.Domain.FootballPitchRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStartEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("FootballPitchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubId");

                    b.HasIndex("FootballPitchId");

                    b.ToTable("FootballPitchRequests");
                });

            modelBuilder.Entity("Domain.Models.Domain.GameClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameClasses");
                });

            modelBuilder.Entity("Domain.Models.Domain.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClubAwayId")
                        .HasColumnType("int");

                    b.Property<int>("ClubHomeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("FootballPitchId")
                        .HasColumnType("int");

                    b.Property<int>("GoalsAway")
                        .HasColumnType("int");

                    b.Property<int>("GoalsHome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubAwayId");

                    b.HasIndex("ClubHomeId");

                    b.HasIndex("FootballPitchId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("Domain.Models.Domain.MatchRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FootballPitchId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FootballPitchId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("MatchRequests");
                });

            modelBuilder.Entity("Domain.Models.Domain.Club", b =>
                {
                    b.HasOne("Domain.Models.Domain.GameClass", "GameClass")
                        .WithMany()
                        .HasForeignKey("GameClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameClass");
                });

            modelBuilder.Entity("Domain.Models.Domain.FootballPitch", b =>
                {
                    b.HasOne("Domain.Models.Domain.Addresses", null)
                        .WithMany("FootballPitches")
                        .HasForeignKey("AddressesId");
                });

            modelBuilder.Entity("Domain.Models.Domain.FootballPitchRequest", b =>
                {
                    b.HasOne("Domain.Models.Domain.Club", "Club")
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Domain.FootballPitch", "FootballPitch")
                        .WithMany()
                        .HasForeignKey("FootballPitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Club");

                    b.Navigation("FootballPitch");
                });

            modelBuilder.Entity("Domain.Models.Domain.Match", b =>
                {
                    b.HasOne("Domain.Models.Domain.Club", "ClubAway")
                        .WithMany()
                        .HasForeignKey("ClubAwayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Domain.Club", "ClubHome")
                        .WithMany()
                        .HasForeignKey("ClubHomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Domain.FootballPitch", "FootballPitch")
                        .WithMany()
                        .HasForeignKey("FootballPitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClubAway");

                    b.Navigation("ClubHome");

                    b.Navigation("FootballPitch");
                });

            modelBuilder.Entity("Domain.Models.Domain.MatchRequest", b =>
                {
                    b.HasOne("Domain.Models.Domain.FootballPitch", "FootballPitch")
                        .WithMany()
                        .HasForeignKey("FootballPitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Domain.Club", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Domain.Club", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FootballPitch");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Domain.Models.Domain.Addresses", b =>
                {
                    b.Navigation("FootballPitches");
                });
#pragma warning restore 612, 618
        }
    }
}
