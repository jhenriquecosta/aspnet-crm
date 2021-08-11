using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ProjectMember : BaseEntity
    { 
       [Required]
       [DisplayName("Project")]
       public int ProjectId { get; set; }

       public virtual Project Project_ProjectId { get; set; }

       [Required]
       [DisplayName("User")]
       public int UserId { get; set; }

       public virtual User User_UserId { get; set; }


    }
}

