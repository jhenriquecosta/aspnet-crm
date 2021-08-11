using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Lead : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       [DisplayName("Position")]
       [StringLength(100)] 
       public string Position { get; set; }

       [DisplayName("Email")]
       [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
       [StringLength(22)] 

       public string Email { get; set; }

       [DisplayName("Website")]
       [StringLength(100)] 
       public string Website { get; set; }

       [DisplayName("Phone")]
       [StringLength(15)] 
       public string Phone { get; set; }

       [DisplayName("Company")]
       [StringLength(100)] 
       public string Company { get; set; }

       [DisplayName("Description")]
       [StringLength(500)] 
       public string Description { get; set; }

       [Required]
       [DisplayName("Lead Status")]
       public int LeadStatusId { get; set; }

       public virtual LeadStatus LeadStatus_LeadStatusId { get; set; }

       [Required]
       [DisplayName("Source")]
       public int SourceId { get; set; }

       public virtual Source Source_SourceId { get; set; }

       [DisplayName("Assigned To User")]
       public int? AssignedToUserId { get; set; }

       public virtual User User_AssignedToUserId { get; set; }

       [Required]
       [DisplayName("On Dated")]
       [Column(TypeName = "datetime")]
       public DateTime OnDated { get; set; }

       [DisplayName("Address")]
       [StringLength(500)] 
       public string Address { get; set; }

       [Required]
       [DisplayName("Country")]
       public int CountryId { get; set; }

       public virtual Country Country_CountryId { get; set; }


    }
}

