using EntityframeWorkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityframeWorkCore.Data
{
    public class DbContextClass : DbContext
    {
        private readonly string connectionString;
        public DbContextClass()
        {
            this.connectionString = "Data Source=localhost\\SQLEXPRESS; Initial Catalog=EmployeeManagement;Integrated Security=True;TrustServerCertificate=true";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder != null)
            {
                optionsBuilder.UseSqlServer(this.connectionString);

                base.OnConfiguring(optionsBuilder);
            }

        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }
        public DbSet<Project> Projects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeProject>().HasKey(ep => new { ep.ProjectId, ep.EmployeeId });

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employees)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
               .HasOne(ep => ep.Projects)
               .WithMany(e => e.EmployeeProjects)
               .HasForeignKey(ep => ep.ProjectId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
