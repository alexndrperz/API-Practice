﻿// <auto-generated />
using JsonCSV.Api.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JsonCSV.Api.Migrations
{
    [DbContext(typeof(CityInfoContext))]
    [Migration("20230401154211_NewUsersTable")]
    partial class NewUsersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("JsonCSV.Api.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Description = "The one with the cathedral that was never really finished.",
                            Name = "Antwerp"
                        },
                        new
                        {
                            Id = 3,
                            Description = "The one with that big tower.",
                            Name = "Paris"
                        });
                });

            modelBuilder.Entity("JsonCSV.Api.Entities.PointOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("pointOfInterests");
                });

            modelBuilder.Entity("JsonCSV.Api.Entities.UsersIdentification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("usersIdentification");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = "WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=",
                            Role = "Admin",
                            UserName = "Name"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = "uGwaNkDuybwje2YFH52Iug8hzhdsw14OqkLN0HLjgxY=",
                            Role = "Admin",
                            UserName = "ww"
                        },
                        new
                        {
                            Id = 3,
                            PasswordHash = "iycDVeU89JiQiiTck7PC9Wx2W18ocSwK2GxPPkuCSsU=",
                            Role = "Admin",
                            UserName = "wwww"
                        });
                });

            modelBuilder.Entity("JsonCSV.Api.Entities.PointOfInterest", b =>
                {
                    b.HasOne("JsonCSV.Api.Entities.City", "City")
                        .WithMany("InterestPoints")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("JsonCSV.Api.Entities.City", b =>
                {
                    b.Navigation("InterestPoints");
                });
#pragma warning restore 612, 618
        }
    }
}
