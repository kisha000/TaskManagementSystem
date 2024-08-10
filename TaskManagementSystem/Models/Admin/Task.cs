using System;

namespace TaskManagementSystem.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        public string TaskDescription { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EstimateDate { get; set; }

        public string PriorityLevel { get; set; }

        public string AttachmentFile { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public bool IsDeleted { get; set; }



        // Navigation properties
        public virtual Project Project { get; set; }

        public virtual Employee Employee { get; set; }
    }
}