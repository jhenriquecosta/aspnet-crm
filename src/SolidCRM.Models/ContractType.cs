using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ContractType : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       public virtual ICollection<Contract> Contract_ContractTypeIds { get; set; }


    }
}

