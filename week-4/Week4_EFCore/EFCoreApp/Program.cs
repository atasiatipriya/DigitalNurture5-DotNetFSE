using EFCoreApp;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("=== EF Core App - Employee Management System ===\n");

// ============================================
// EXERCISE 1: READ - Get all data
// ============================================
Console.WriteLine("--- Exercise 1: READ All Employees ---");
using (var context = new AppDbContext())
{
    var employees = context.Employees
        .Include(e => e.Department)  // Load related Department too
        .ToList();

    foreach (var emp in employees)
    {
        Console.WriteLine($"ID: {emp.EmployeeId} | Name: {emp.Name} | " +
                         $"Dept: {emp.Department.DepartmentName} | " +
                         $"Salary: Rs.{emp.Salary}");
    }
}

// ============================================
// EXERCISE 2: CREATE - Add new employee
// ============================================
Console.WriteLine("\n--- Exercise 2: CREATE New Employee ---");
using (var context = new AppDbContext())
{
    var newEmployee = new Employee
    {
        Name = "Ravi Patel",
        Email = "ravi@company.com",
        Salary = 60000,
        JoiningDate = DateTime.Now,
        DepartmentId = 1  // Engineering
    };

    context.Employees.Add(newEmployee);
    context.SaveChanges();
    Console.WriteLine($"New employee added: {newEmployee.Name} with ID: {newEmployee.EmployeeId}");
}

// ============================================
// EXERCISE 3: UPDATE - Modify employee salary
// ============================================
Console.WriteLine("\n--- Exercise 3: UPDATE Employee Salary ---");
using (var context = new AppDbContext())
{
    // Find employee by ID
    var employee = context.Employees.FirstOrDefault(e => e.Name == "Ravi Patel");

    if (employee != null)
    {
        double oldSalary = (double)employee.Salary;
        employee.Salary = 75000;
        context.SaveChanges();
        Console.WriteLine($"Updated {employee.Name}'s salary: Rs.{oldSalary} → Rs.{employee.Salary}");
    }
}

// ============================================
// EXERCISE 4: LINQ QUERIES
// ============================================
Console.WriteLine("\n--- Exercise 4: LINQ Queries ---");
using (var context = new AppDbContext())
{
    // Query 1: Find employees with salary above 70000
    Console.WriteLine("\nEmployees with salary above Rs.70000:");
    var highEarners = context.Employees
        .Where(e => e.Salary > 70000)
        .OrderByDescending(e => e.Salary)
        .ToList();

    foreach (var emp in highEarners)
        Console.WriteLine($"  {emp.Name} - Rs.{emp.Salary}");

    // Query 2: Count employees per department
    Console.WriteLine("\nEmployee count per Department:");
    var deptCount = context.Employees
        .Include(e => e.Department)
        .GroupBy(e => e.Department.DepartmentName)
        .Select(g => new { Department = g.Key, Count = g.Count() })
        .ToList();

    foreach (var item in deptCount)
        Console.WriteLine($"  {item.Department}: {item.Count} employees");

    // Query 3: Average salary per department
    Console.WriteLine("\nAverage salary per Department:");
    var avgSalary = context.Employees
        .Include(e => e.Department)
        .GroupBy(e => e.Department.DepartmentName)
        .Select(g => new { Department = g.Key, AvgSalary = g.Average(e => e.Salary) })
        .ToList();

    foreach (var item in avgSalary)
        Console.WriteLine($"  {item.Department}: Rs.{item.AvgSalary:F2}");
}

// ============================================
// EXERCISE 5: DELETE - Remove an employee
// ============================================
Console.WriteLine("\n--- Exercise 5: DELETE Employee ---");
using (var context = new AppDbContext())
{
    var employee = context.Employees
        .FirstOrDefault(e => e.Name == "Ravi Patel");

    if (employee != null)
    {
        context.Employees.Remove(employee);
        context.SaveChanges();
        Console.WriteLine($"Employee '{employee.Name}' deleted successfully!");
    }
}

// ============================================
// FINAL: Show all employees after operations
// ============================================
Console.WriteLine("\n--- Final Employee List ---");
using (var context = new AppDbContext())
{
    var employees = context.Employees
        .Include(e => e.Department)
        .OrderBy(e => e.DepartmentId)
        .ToList();

    foreach (var emp in employees)
    {
        Console.WriteLine($"  {emp.Name} | {emp.Department.DepartmentName} | Rs.{emp.Salary}");
    }
}

Console.WriteLine("\n=== All EF Core exercises completed! ===");