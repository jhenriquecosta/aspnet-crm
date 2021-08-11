using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Ticket : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(150)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Ticket Detail")]
       public string TicketDetail { get; set; }

       [Required]
       [DisplayName("Start Date")]
       [Column(TypeName = "datetime")]
       public DateTime StartDate { get; set; }

       [DisplayName("End Date")]
       [Column(TypeName = "datetime")]
       public DateTime? EndDate { get; set; }

       [Required]
       [DisplayName("Company Client")]
       public int CompanyClientId { get; set; }

       public virtual CompanyClient CompanyClient_CompanyClientId { get; set; }

       [Required]
       [DisplayName("Status")]
       public int StatusId { get; set; }

       public virtual Status Status_StatusId { get; set; }

       [Required]
       [DisplayName("Priority")]
       public int PriorityId { get; set; }

       public virtual Priority Priority_PriorityId { get; set; }

       [Required]
       [DisplayName("Company")]
       public int CompanyId { get; set; }

       public virtual Company Company_CompanyId { get; set; }

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

       public virtual ICollection<TicketTask> TicketTask_TicketIds { get; set; }


    }
}

