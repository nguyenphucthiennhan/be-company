using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
public class EmployeeType
{
    [Key]
    public int TypeID { get; set; }
    public string TypeName { get; set; }
}
}