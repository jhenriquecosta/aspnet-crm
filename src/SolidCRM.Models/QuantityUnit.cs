using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class QuantityUnit : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       public string Title { get; set; }

       public virtual ICollection<InvoiceItem> InvoiceItem_QuantityUnitIds { get; set; }


    }
}

