using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementSystem.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; }

        public string Status { get; set; } = "Open";

        public string PriorityLevel { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
        public List<string> SelectedEmployees { get; set; }

        public IEnumerable<Employee> ProjectEmployees { get; set; }
    }
}