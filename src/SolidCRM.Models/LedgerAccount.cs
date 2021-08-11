using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class LedgerAccount : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(50)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Currency")]
       public int CurrencyId { get; set; }

       [DisplayName("Account Code")]
       [StringLength(50)] 
       public string AccountCode { get; set; }

       [DisplayName("Account Color")]
       [StringLength(10)] 
       public string AccountColor { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual LedgerAccount LedgerAccount2 { get; set; }

       [Required]
       [DisplayName("Company")]
       public int CompanyId { get; set; }

       public virtual Company Company_CompanyId { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

       public virtual ICollection<Transaction> Transaction_DebitLedgerAccountIds { get; set; }

       public virtual ICollection<Transaction> Transaction_CreditLedgerAccountIds { get; set; }

       public virtual ICollection<LedgerAccount> LedgerAccount_ParentIds { get; set; }


    }
}

