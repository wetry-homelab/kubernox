﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    partial class ServiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Application.Entities.Cluster", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("BaseTemplate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Cpu")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValue(new DateTime(2021, 3, 27, 9, 53, 16, 766, DateTimeKind.Utc).AddTicks(6744));

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KubeConfig")
                        .HasColumnType("text");

                    b.Property<string>("KubeConfigJson")
                        .HasColumnType("text");

                    b.Property<int>("Memory")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Node")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProxmoxNodeId")
                        .HasColumnType("integer");

                    b.Property<int>("SshKeyId")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Provisionning");

                    b.Property<int>("Storage")
                        .HasColumnType("integer");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SshKeyId");

                    b.ToTable("Cluster");
                });

            modelBuilder.Entity("Application.Entities.ClusterDomain", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ClusterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FullChain")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PrivateKey")
                        .HasColumnType("text");

                    b.Property<string>("Protocol")
                        .HasColumnType("text");

                    b.Property<string>("Resolver")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RootDomainId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ClusterDomain");
                });

            modelBuilder.Entity("Application.Entities.ClusterNode", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ClusterId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClusterId");

                    b.ToTable("ClusterNode");
                });

            modelBuilder.Entity("Application.Entities.DatacenterNode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Core")
                        .HasColumnType("integer");

                    b.Property<string>("Flag")
                        .HasColumnType("text");

                    b.Property<bool>("Hvm")
                        .HasColumnType("boolean");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("KernelVersion")
                        .HasColumnType("text");

                    b.Property<double>("Mhz")
                        .HasColumnType("double precision");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Online")
                        .HasColumnType("boolean");

                    b.Property<string>("PveVersion")
                        .HasColumnType("text");

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
                        .HasColumnType("integer");

                    b.Property<long>("SwapFree")
                        .HasColumnType("bigint");

                    b.Property<long>("SwapTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("SwapUsed")
                        .HasColumnType("bigint");

                    b.Property<int>("Thread")
                        .HasColumnType("integer");

                    b.Property<long>("Uptime")
                        .HasColumnType("bigint");

                    b.Property<int>("UserHz")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("DatacenterNode");
                });

            modelBuilder.Entity("Application.Entities.Domain", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("ValidationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ValidationKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Domain");
                });

            modelBuilder.Entity("Application.Entities.Metric", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("CpuValue")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EntityId")
                        .HasColumnType("text");

                    b.Property<long>("MemoryValue")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Metric");
                });

            modelBuilder.Entity("Application.Entities.SshKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Fingerprint")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Pem")
                        .HasColumnType("text");

                    b.Property<string>("Private")
                        .HasColumnType("text");

                    b.Property<string>("Public")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SshKey");
                });

            modelBuilder.Entity("Application.Entities.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BaseTemplate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CpuCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DiskSpace")
                        .HasColumnType("integer");

                    b.Property<int>("MemoryCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Template");
                });

            modelBuilder.Entity("Application.Entities.TraefikRouteValue", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ClusterId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Domain")
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RuleId")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TraefikRouteValue");
                });

            modelBuilder.Entity("Application.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .HasColumnType("boolean");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Application.Entities.Cluster", b =>
                {
                    b.HasOne("Application.Entities.SshKey", "SshKey")
                        .WithMany("Clusters")
                        .HasForeignKey("SshKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SshKey");
                });

            modelBuilder.Entity("Application.Entities.ClusterNode", b =>
                {
                    b.HasOne("Application.Entities.Cluster", "Cluster")
                        .WithMany("Nodes")
                        .HasForeignKey("ClusterId");

                    b.Navigation("Cluster");
                });

            modelBuilder.Entity("Application.Entities.Cluster", b =>
                {
                    b.Navigation("Nodes");
                });

            modelBuilder.Entity("Application.Entities.SshKey", b =>
                {
                    b.Navigation("Clusters");
                });
#pragma warning restore 612, 618
        }
    }
}
