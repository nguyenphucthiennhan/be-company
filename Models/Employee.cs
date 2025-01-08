
using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
public class Employee
{
     [Key]
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? DepartmentID { get; set; }
    public string Position { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int? TypeID { get; set; }
    public DateTime DayOfBirth { get; set; }
    public string Address { get; set; }

    public Department Department { get; set; }
    public EmployeeType EmployeeType { get; set; }
}

}