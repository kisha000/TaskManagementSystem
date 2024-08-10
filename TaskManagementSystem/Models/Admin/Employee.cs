using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagementSystem.Models.Admin;

namespace TaskManagementSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public int? RoleId { get; set; }

        public bool IsDeleted { get; set; }

        public int? ProjectId { get; set; }

        public string RoleName { get; set; }

        public string ProjectName { get; set; }

        [ForeignKey("RoleId")]
        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
