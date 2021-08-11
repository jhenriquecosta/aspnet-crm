using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Status : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(100)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Color Name")]
       [StringLength(100)] 
       public string ColorName { get; set; }

       [DisplayName("Sort Order")]
       public int? SortOrder { get; set; }

       public virtual ICollection<Ticket> Ticket_StatusIds { get; set; }

       public virtual ICollection<TicketTask> TicketTask_StatusIds { get; set; }

       public virtual ICollection<Project> Project_StatusIds { get; set; }

       public virtual ICollection<ProjectTask> ProjectTask_StatusIds { get; set; }


    }
}

