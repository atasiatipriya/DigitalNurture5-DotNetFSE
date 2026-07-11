using Microsoft.EntityFrameworkCore;

namespace EFCoreApp
{
    // DbContext is the bridge between your C# classes
    // and the actual database
    public class AppDbContext : DbContext
    {
        // DbSet represents a table in the database
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        // Tell EF Core which database to connect to
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                @"Server=ATASI\SQLEXPRESS;Database=EFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        // Seed initial data when database is created
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "Engineering", Location = "Bhubaneswar" },
                new Department { DepartmentId = 2, DepartmentName = "HR", Location = "Delhi" },
                new Department { DepartmentId = 3, DepartmentName = "Finance", Location = "Mumbai" }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Atasi Priya", Email = "atasi@company.com", Salary = 75000, JoiningDate = new DateTime(2024, 1, 15), DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Rahul Sharma", Email = "rahul@company.com", Salary = 65000, JoiningDate = new DateTime(2023, 6, 1), DepartmentId = 1 },
                new Employee { EmployeeId = 3, Name = "Priya Singh", Email = "priya@company.com", Salary = 55000, JoiningDate = new DateTime(2023, 3, 20), DepartmentId = 2 },
                new Employee { EmployeeId = 4, Name = "Amit Kumar", Email = "amit@company.com", Salary = 80000, JoiningDate = new DateTime(2022, 9, 10), DepartmentId = 3 },
                new Employee { EmployeeId = 5, Name = "Sneha Das", Email = "sneha@company.com", Salary = 70000, JoiningDate = new DateTime(2024, 2, 28), DepartmentId = 2 }
            );
        }
    }
}