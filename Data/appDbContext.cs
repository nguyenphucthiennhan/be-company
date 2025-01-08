using Microsoft.EntityFrameworkCore;
using be_company.Models;

namespace be_company.Data  // Nếu bạn sử dụng thư mục Data
{
    public class AppDbContext : DbContext
    {
        // Constructor mặc định nhận options để cấu hình DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Các DbSet đại diện cho các bảng trong cơ sở dữ liệu
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

            // Định nghĩa các quan hệ (Foreign Keys) nếu không được tự động phát hiện
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeType)
                .WithMany()
                .HasForeignKey(e => e.TypeID);

            // Định nghĩa các quan hệ foreign key cho các entity còn lại
            modelBuilder.Entity<LeaveBalance>()
                .HasOne(l => l.Employee)
                .WithOne()
                .HasForeignKey<LeaveBalance>(l => l.EmployeeId);
            
            // Cấu hình các quan hệ khác cho các bảng còn lại
            // modelBuilder.Entity<OtherEntity>().HasOne(...) ví dụ thêm mối quan hệ tương tự cho các bảng khác.
        }
    }
}
