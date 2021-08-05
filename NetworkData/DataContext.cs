using Microsoft.EntityFrameworkCore;
using NetworkData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option)
           : base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Gateway>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Device>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            // Seed data when model intialize.
            //modelBuilder.Entity<Gateway>().HasData(
            //    new Gateway
            //    {
            //        Name = "Gateway 2",
            //        IP4Address = "255.255.225.101",
            //        SerialNumer = "Seri1234"
            //    });
            */
        }
        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
    }
}
