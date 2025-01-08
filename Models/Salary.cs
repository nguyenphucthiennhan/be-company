
using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
    public class Salaries
{
     [Key]
    public int EmployeeID { get; set; }
    public decimal? Salary { get; set; }
    public decimal? Bonus { get; set; }
    public DateTime PaymentDate { get; set; }

    public Employee Employee { get; set; }
}
}
