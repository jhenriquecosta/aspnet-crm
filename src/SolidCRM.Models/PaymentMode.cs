using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class PaymentMode : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(50)] 
       public string Title { get; set; }

       public virtual ICollection<Invoice> Invoice_PaymentModeIds { get; set; }


    }
}

