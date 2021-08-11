using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class InvoiceItem : BaseEntity
    { 
       [Required]
       [DisplayName("Invoice")]
       public int InvoiceId { get; set; }

       public virtual Invoice Invoice_InvoiceId { get; set; }

       [DisplayName("Description")]
       [StringLength(200)] 
       public string Description { get; set; }

       [Required]
       [DisplayName("Title")]
       [StringLength(50)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Quantity")]
       public Decimal Quantity { get; set; }

       [Required]
       [DisplayName("Unit Price")]
       public Decimal UnitPrice { get; set; }

       [Required]
       [DisplayName("Quantity Unit")]
       public int QuantityUnitId { get; set; }

       public virtual QuantityUnit QuantityUnit_QuantityUnitId { get; set; }

       [Required]
       [DisplayName("Total")]
       public Decimal Total { get; set; }

       [DisplayName("Tax")]
       public Decimal? Tax { get; set; }

       [DisplayName("Discount")]
       public Decimal? Discount { get; set; }

       [DisplayName("Adjustment")]
       public Decimal? Adjustment { get; set; }


    }
}

