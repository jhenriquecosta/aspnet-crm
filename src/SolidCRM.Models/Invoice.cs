using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Invoice : BaseEntity
    { 
       [Required]
       [DisplayName("Bill Date")]
       [Column(TypeName = "datetime")]
       public DateTime BillDate { get; set; }

       [DisplayName("Due Date")]
       [Column(TypeName = "datetime")]
       public DateTime? DueDate { get; set; }

       [Required]
       [DisplayName("Payment Mode")]
       public int PaymentModeId { get; set; }

       public virtual PaymentMode PaymentMode_PaymentModeId { get; set; }

       [Required]
       [DisplayName("To")]
       [StringLength(100)] 
       public string To { get; set; }

       [DisplayName("Other Invoice Code")]
       [StringLength(50)] 
       public string OtherInvoiceCode { get; set; }

       [DisplayName("Address")]
       [StringLength(200)] 
       public string Address { get; set; }

       [Required]
       [DisplayName("Created By")]
       public int CreatedBy { get; set; }

       [DisplayName("Company")]
       public int? CompanyId { get; set; }

       public virtual Company Company_CompanyId { get; set; }

       [Required]
       [DisplayName("Country")]
       public int CountryId { get; set; }

       public virtual Country Country_CountryId { get; set; }

       [DisplayName("Zip Code")]
       [StringLength(10)] 
       public string ZipCode { get; set; }

       [DisplayName("Email")]
       [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
       [StringLength(40)] 

       public string Email { get; set; }

       [DisplayName("Mobile")]
       [StringLength(15)] 
       public string Mobile { get; set; }

       [DisplayName("Client Address")]
       public int? ClientAddressId { get; set; }

       public virtual ClientAddress ClientAddress_ClientAddressId { get; set; }

       public virtual ICollection<InvoiceItem> InvoiceItem_InvoiceIds { get; set; }


    }
}

