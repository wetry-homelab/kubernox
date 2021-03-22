﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence.Contexts
{
    public class ServiceDbContext : DbContext
    {
        public DbSet<Cluster> Cluster { get; set; }
        public DbSet<ClusterNode> ClusterNode { get; set; }
        public DbSet<DatacenterNode> DatacenterNode { get; set; }
        public DbSet<DomainName> DomainName { get; set; }
        public DbSet<Metric> Metric { get; set; }
        public DbSet<SshKey> SshKey { get; set; }
        public DbSet<SubDomain> SubDomain { get; set; }
        public DbSet<Template> Template { get; set; }
        public DbSet<User> User { get; set; }

        public ServiceDbContext(DbContextOptions<ServiceDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cluster>().Property(p => p.State).HasDefaultValue("Provisionning");
            modelBuilder.Entity<Cluster>().Property(p => p.CreateAt).HasDefaultValue(DateTime.UtcNow);

            modelBuilder.Entity<Cluster>()
                        .HasMany(c => c.Nodes)
                        .WithOne(n => n.Cluster);

            modelBuilder.Entity<Cluster>()
                        .HasOne(c => c.SshKey)
                        .WithMany(n => n.Clusters);

            modelBuilder.Entity<DomainName>()
                        .HasMany(c => c.SubDomains)
                        .WithOne(n => n.DomainName);
        }
    }
}
