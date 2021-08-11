using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class TicketTask : BaseEntity
    { 
       [Required]
       [DisplayName("Ticket")]
       public int TicketId { get; set; }

       public virtual Ticket Ticket_TicketId { get; set; }

       [Required]
       [DisplayName("Start Date")]
       [Column(TypeName = "datetime")]
       public DateTime StartDate { get; set; }

       [DisplayName("End Date")]
       [Column(TypeName = "datetime")]
       public DateTime? EndDate { get; set; }

       [Required]
       [DisplayName("Task Detail")]
       [StringLength(1000)] 
       public string TaskDetail { get; set; }

       [Required]
       [DisplayName("Status")]
       public int StatusId { get; set; }

       public virtual Status Status_StatusId { get; set; }

       [Required]
       [DisplayName("Priority")]
       public int PriorityId { get; set; }

       public virtual Priority Priority_PriorityId { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Date Modied")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModied { get; set; }

       [DisplayName("Is Delete")]
       public bool? IsDelete { get; set; }

       [DisplayName("Take Time In Hour")]
       public Decimal? TakeTimeInHour { get; set; }


    }
}

