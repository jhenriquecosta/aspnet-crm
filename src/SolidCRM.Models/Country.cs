using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Country : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       public virtual ICollection<Lead> Lead_CountryIds { get; set; }

       public virtual ICollection<Invoice> Invoice_CountryIds { get; set; }

       public virtual ICollection<ClientAddress> ClientAddress_CountryIds { get; set; }

       public virtual ICollection<CompanyClient> CompanyClient_CountryIds { get; set; }


    }
}

