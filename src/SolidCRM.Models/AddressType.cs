using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class AddressType : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       public virtual ICollection<ClientAddress> ClientAddress_AddressTypeIds { get; set; }


    }
}

