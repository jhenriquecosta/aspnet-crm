using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Project : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(200)] 
       public string Name { get; set; }

       [Required]
       [DisplayName("Added By")]
       public int AddedBy { get; set; }

       [DisplayName("Start Date")]
       [Column(TypeName = "datetime")]
       public DateTime? StartDate { get; set; }

       [Required]
       [DisplayName("End Date")]
       [Column(TypeName = "datetime")]
       public DateTime EndDate { get; set; }

       [Required]
       [DisplayName("Company Office")]
       public int CompanyOfficeId { get; set; }

       [Required]
       [DisplayName("Status")]
       public string Status { get; set; }

       [DisplayName("Company Client")]
       public int? CompanyClientId { get; set; }

       public virtual CompanyClient CompanyClient_CompanyClientId { get; set; }

       [DisplayName("Details")]
       [StringLength(2000)] 
       public string Details { get; set; }

       [DisplayName("Target Date")]
       [Column(TypeName = "datetime")]
       public DateTime? TargetDate { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Date Modified")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModified { get; set; }

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

       [DisplayName("Tags")]
       [StringLength(100)] 
       public string Tags { get; set; }

       [DisplayName("Amount")]
       public Decimal? Amount { get; set; }

       public virtual ICollection<ProjectMileStone> ProjectMileStone_ProjectIds { get; set; }

       public virtual ICollection<ProjectMember> ProjectMember_ProjectIds { get; set; }

       public virtual ICollection<ProjectTask> ProjectTask_ProjectIds { get; set; }

       public virtual ICollection<ProjectFile> ProjectFile_ProjectIds { get; set; }


    }
}

