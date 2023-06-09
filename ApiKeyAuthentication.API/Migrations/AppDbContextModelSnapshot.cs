﻿// <auto-generated />
using System;
using ApiKeyAuthentication.API.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiKeyAuthentication.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.ApiKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ApiKeys");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"),
                            Active = false,
                            ClientId = new Guid("b1e210d3-2159-4108-b57b-2d0645d87a2f"),
                            Value = "c/lwddFKxJt7J/S3xGBuJyNQ5VSK5js59pBkCdpZ"
                        });
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.ApiKeyPermission", b =>
                {
                    b.Property<Guid>("ApiKeyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("ApiKeyId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("ApiKeyPermissions");

                    b.HasData(
                        new
                        {
                            ApiKeyId = new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"),
                            PermissionId = 1
                        },
                        new
                        {
                            ApiKeyId = new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"),
                            PermissionId = 2
                        },
                        new
                        {
                            ApiKeyId = new Guid("9556c87b-4119-4aa1-acaf-3229a6aae8c7"),
                            PermissionId = 3
                        });
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b1e210d3-2159-4108-b57b-2d0645d87a2f"),
                            Name = "test client"
                        });
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "comments.read"
                        },
                        new
                        {
                            Id = 2,
                            Value = "comments.write"
                        },
                        new
                        {
                            Id = 3,
                            Value = "blogs.read"
                        },
                        new
                        {
                            Id = 4,
                            Value = "blogs.write"
                        });
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.ApiKey", b =>
                {
                    b.HasOne("ApiKeyAuthentication.API.Data.Entities.Client", "Client")
                        .WithMany("ApiKeys")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.ApiKeyPermission", b =>
                {
                    b.HasOne("ApiKeyAuthentication.API.Data.Entities.ApiKey", "ApiKey")
                        .WithMany("Permissions")
                        .HasForeignKey("ApiKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiKeyAuthentication.API.Data.Entities.Permission", "Permission")
                        .WithMany("ApiKeys")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApiKey");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.ApiKey", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.Client", b =>
                {
                    b.Navigation("ApiKeys");
                });

            modelBuilder.Entity("ApiKeyAuthentication.API.Data.Entities.Permission", b =>
                {
                    b.Navigation("ApiKeys");
                });
#pragma warning restore 612, 618
        }
    }
}
