﻿// <auto-generated />
using System;
using Kubernox.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kubernox.Infrastructure.Migrations
{
    [DbContext(typeof(KubernoxDbContext))]
    [Migration("20231120150234_AddTemplateNodeIdField")]
    partial class AddTemplateNodeIdField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Kubernox.Domain.Entities.HostConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ApiToken")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeleteBy")
                        .HasColumnType("text");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("HostConfiguration", (string)null);
                });

            modelBuilder.Entity("Kubernox.Domain.Entities.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("Kubernox.Domain.Entities.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeleteBy")
                        .HasColumnType("text");

                    b.Property<Guid>("HostConfigurationId")
                        .HasColumnType("uuid");

                    b.Property<long?>("MaxCpu")
                        .HasColumnType("bigint");

                    b.Property<long?>("MaxMemory")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NodeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Node", (string)null);
                });

            modelBuilder.Entity("Kubernox.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("Kubernox.Domain.Entities.SshKey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreateBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeleteBy")
                        .HasColumnType("text");

                    b.Property<string>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdateBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SshKey", (string)null);
                });

            modelBuilder.Entity("Kubernox.Domain.Entities.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("HostConfigurationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("NodeId")
                        .HasColumnType("uuid");

                    b.Property<long>("VmId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Template", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
