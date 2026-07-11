using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreApp
{
    // ============================================
    // DEPARTMENT MODEL
    // One Department has Many Employees
    // ============================================
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        public string Location { get; set; }

        // Navigation property - one department has many employees
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

    // ============================================
    // EMPLOYEE MODEL
    // Many Employees belong to One Department
    // ============================================
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        public DateTime JoiningDate { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation property - employee belongs to one department
        public Department Department { get; set; }
    }
}