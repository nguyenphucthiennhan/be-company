using Microsoft.EntityFrameworkCore;
using be_company.Models;

namespace be_company.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmploymentContract> EmploymentContracts { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<PerformanceKPI> PerformanceKPIs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeTracking> TimeTracking { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Employee-Department relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentID);

            // Employee-EmployeeType relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeType)
                .WithMany()
                .HasForeignKey(e => e.TypeID);

            // LeaveBalance-Employee relationship (One-to-One)
            modelBuilder.Entity<LeaveBalance>()
                .HasOne(l => l.Employee)
                .WithOne()
                .HasForeignKey<LeaveBalance>(l => l.EmployeeId);

            // ProjectDetail-Client relationship
            modelBuilder.Entity<ProjectDetail>()
                .HasOne(pd => pd.Client)
                .WithMany()  // Client can have many ProjectDetails
                .HasForeignKey(pd => pd.ClientID);

            // ProjectDetail-Project relationship
            modelBuilder.Entity<ProjectDetail>()
                .HasOne(pd => pd.Project)
                .WithMany()  // Project can have many ProjectDetails
                .HasForeignKey(pd => pd.ProjectId);

            // EmployeeProject-Project relationship
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany()  // Project can have many EmployeeProjects
                .HasForeignKey(ep => ep.ProjectId);

            // EmployeeProject-Employee relationship
            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany()  // Employee can have many EmployeeProjects
                .HasForeignKey(ep => ep.EmployeeId);

            // Insurance-Employee relationship
            modelBuilder.Entity<Insurance>()
                .HasOne(i => i.Employee)
                .WithMany()  // Employee can have many Insurances
                .HasForeignKey(i => i.EmployeeID);

            // Leave-Employee relationship
            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany()  // Employee can have many Leaves
                .HasForeignKey(l => l.EmployeeID);

            // User-Employee relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithMany()  // Employee can have many Users
                .HasForeignKey(u => u.EmployeeId);
        }
    }
}
