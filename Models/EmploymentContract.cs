using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
    public class EmploymentContract
{
     [Key]
    public int ContractID { get; set; }
    public int? EmployeeID { get; set; }
    public string ContractType { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Employee Employee { get; set; }
}
}