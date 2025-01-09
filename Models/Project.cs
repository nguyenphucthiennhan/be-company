using System;
using System.ComponentModel.DataAnnotations;

namespace be_company.Models // Thay "be_company" bằng tên namespace của bạn
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; } // Khóa chính (sẽ tương ứng với ProjectId trong cơ sở dữ liệu)
        public string ProjectName { get; set; } // Tên dự án
        public DateTime? StartDate { get; set; } // Ngày bắt đầu
        public DateTime? EndDate { get; set; } // Ngày kết thúc
        public string Status { get; set; } // Trạng thái dự án
        public int? ManagerID { get; set; } // ID của người quản lý (Khóa ngoại)
        public ProjectDetail ProjectDetails { get; set; }

    }
}
