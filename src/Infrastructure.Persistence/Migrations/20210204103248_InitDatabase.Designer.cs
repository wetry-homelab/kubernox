﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20210204103248_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Domain.Entities.Cluster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cpu")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 2, 4, 10, 32, 48, 368, DateTimeKind.Utc).AddTicks(354));

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Memory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Node")
                        .HasColumnType("int");

                    b.Property<string>("SshKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Provisionning");

                    b.Property<int>("Storage")
                        .HasColumnType("int");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cluster");
                });

            modelBuilder.Entity("Domain.Entities.DatacenterNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Core")
                        .HasColumnType("int");

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Hvm")
                        .HasColumnType("bit");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KernelVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Mhz")
                        .HasColumnType("float");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Online")
                        .HasColumnType("bit");

                    b.Property<string>("PveVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RamFree")
                        .HasColumnType("bigint");

                    b.Property<long>("RamTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("RamUsed")
                        .HasColumnType("bigint");

                    b.Property<long>("RootFsAvailable")
                        .HasColumnType("bigint");

                    b.Property<long>("RootFsFree")
                        .HasColumnType("bigint");

                    b.Property<long>("RootFsTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("RootFsUsed")
                        .HasColumnType("bigint");

                    b.Property<int>("Socket")
                        .HasColumnType("int");

                    b.Property<long>("SwapFree")
                        .HasColumnType("bigint");

                    b.Property<long>("SwapTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("SwapUsed")
                        .HasColumnType("bigint");

                    b.Property<int>("Thread")
                        .HasColumnType("int");

                    b.Property<int>("Uptime")
                        .HasColumnType("int");

                    b.Property<int>("UserHz")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DatacenterNode");
                });
#pragma warning restore 612, 618
        }
    }
}
