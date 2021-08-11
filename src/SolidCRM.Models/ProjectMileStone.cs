using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ProjectMileStone : BaseEntity
    { 
       [Required]
       [DisplayName("Project")]
       public int ProjectId { get; set; }

       public virtual Project Project_ProjectId { get; set; }

       [DisplayName("Name")]
       [StringLength(200)] 
       public string Name { get; set; }

       [Required]
       [DisplayName("Amount")]
       public Decimal Amount { get; set; }

       public virtual ICollection<ProjectTask> ProjectTask_ProjectMileStoneIds { get; set; }


    }
}

