
using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
    public class PerformanceKPI
{
     [Key]
    public int KPIID { get; set; }
    public int? EmployeeID { get; set; }
    public decimal? KPIValue { get; set; }
    public DateTime? KPIMonth { get; set; }

    public Employee Employee { get; set; }
}
public class TimeTracking
{
     [Key]
    public int EntryID { get; set; }
    public int? EmployeeID { get; set; }
    public DateTime? Date { get; set; }
    public decimal? HoursWorked { get; set; }

    public Employee Employee { get; set; }
}
public class Client
{
     [Key]
    public int ClientID { get; set; }
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string ContactEmail { get; set; }
    public string PhoneNumber { get; set; }
}
public class ProjectDetail
{
     [Key]
    public int DetailsId { get; set; }
    public int? ClientID { get; set; }
    public string DetailedDescription { get; set; }
    public decimal? EstimatedBudget { get; set; }
    public decimal? ActualBudget { get; set; }
    public int ProjectId { get; set; }
    public string SRS { get; set; }

    public Client Client { get; set; }
    public Project Project { get; set; }
}
public class EmployeeProject
{
     [Key]
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }

    public Project Project { get; set; }
    public Employee Employee { get; set; }
}
public class Insurance
{
     [Key]
    public int InsuranceCode { get; set; }
    public int EmployeeID { get; set; }
    public string Type { get; set; }
    public int Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
    public decimal CoverageAmount { get; set; }

    public Employee Employee { get; set; }
}
public class Leave
{
     [Key]
    public int LeaveId { get; set; }
    public int EmployeeID { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Reason { get; set; }
    public int Status { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }

    public Employee Employee { get; set; }
}
public class User
{
     [Key]
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Email { get; set; }
    public string PassWord { get; set; }
    public DateTime Created_at { get; set; }
    public DateTime Updated_at { get; set; }
    public int Role { get; set; }

    public Employee Employee { get; set; }
}
 public class LeaveBalance
    {
         [Key]
        public int EmployeeId { get; set; }  // Foreign key to the Employee
        public int TotalLeaveDays { get; set; }  // Total leave days available for the employee
        public int UsedLeaveDays { get; set; }  // Total leave days used by the employee
        public int RemainingLeaveDays => TotalLeaveDays - UsedLeaveDays;  // Computed property for remaining leave days
        public DateTime UpdatedAt { get; set; }  // Timestamp for when the record was last updated

        // Navigation property to reference the Employee associated with this record
        public Employee Employee { get; set; }
    }
}