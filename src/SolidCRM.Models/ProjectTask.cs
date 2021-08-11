using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ProjectTask : BaseEntity
    { 
       [Required]
       [DisplayName("Project")]
       public int ProjectId { get; set; }

       public virtual Project Project_ProjectId { get; set; }

       [Required]
       [DisplayName("Task Names")]
       [StringLength(100)] 
       public string TaskNames { get; set; }

       [Required]
       [DisplayName("Description")]
       public string Description { get; set; }

       [Required]
       [DisplayName("User")]
       public int UserId { get; set; }

       [DisplayName("Start Date")]
       [Column(TypeName = "datetime")]
       public DateTime? StartDate { get; set; }

       [DisplayName("End Date")]
       [Column(TypeName = "datetime")]
       public DateTime? EndDate { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Date Modified")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModified { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [Required]
       [DisplayName("Status")]
       public int StatusId { get; set; }

       public virtual Status Status_StatusId { get; set; }

       [Required]
       [DisplayName("Priority")]
       public int PriorityId { get; set; }

       public virtual Priority Priority_PriorityId { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual ProjectTask ProjectTask2 { get; set; }

       [DisplayName("Project Mile Stone")]
       public int? ProjectMileStoneId { get; set; }

       public virtual ProjectMileStone ProjectMileStone_ProjectMileStoneId { get; set; }

       public virtual ICollection<ProjectTask> ProjectTask_ParentIds { get; set; }


    }
}

