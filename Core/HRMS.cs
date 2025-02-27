#nullable enable

using System;
using System.Collections.Generic;

namespace Core
{
    // Department: Represents organizational units or divisions.
    public class Department : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        // A department can have multiple employees.
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

    // Employee: Stores employee details.
    public class Employee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string? JobTitle { get; set; }

        // Foreign key to Department.
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        // Navigation properties for HRMS-related data.
        public Salary? Salary { get; set; }
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }

    // Attendance: Logs daily attendance records.
    public class Attendance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan? CheckIn { get; set; }
        public TimeSpan? CheckOut { get; set; }
        public bool IsAbsent { get; set; }
    }

    // Leave: Records leave requests and their statuses.
    public class Leave : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? LeaveType { get; set; }  // e.g., Sick, Vacation, etc.
        public string? Reason { get; set; }
        public bool Approved { get; set; }
    }

    // Salary: Holds salary details for an employee.
    public class Salary : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowances { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    // Payroll: Captures payroll processing details.
    public class Payroll : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime PayrollDate { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public string? Remarks { get; set; }
    }
}
