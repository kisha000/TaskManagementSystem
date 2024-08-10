using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models.Admin
{
    public class ProjectEmployee
    {
        [Key]
        public int ProjectEmployeeId { get; set; }

        public int ProjectId { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("ProjectId")]
        public virtual Project ProjectName { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee EmployeeName { get; set; }
    }
}