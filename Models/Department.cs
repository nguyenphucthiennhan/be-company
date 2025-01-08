
using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
    public class Department
{
     [Key]
    public int DepartmentID { get; set; }
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public DateTime? ActiveFrom { get; set; }
    public DateTime? ActiveTo { get; set; }
}
}