using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class LeadStatus : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       public virtual ICollection<Lead> Lead_LeadStatusIds { get; set; }


    }
}

