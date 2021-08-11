using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Currency : BaseEntity
    { 
       [DisplayName("Name")]
       [StringLength(30)] 
       public string Name { get; set; }

       [DisplayName("Code")]
       [StringLength(20)] 
       public string Code { get; set; }

       public virtual ICollection<CompanyClient> CompanyClient_CurrencyIds { get; set; }


    }
}

