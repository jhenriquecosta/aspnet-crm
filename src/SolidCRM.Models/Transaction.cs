using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Transaction : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(200)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime DateAdded { get; set; }

       [Required]
       [DisplayName("Added By")]
       public int AddedBy { get; set; }

       [Required]
       [DisplayName("Company")]
       public int CompanyId { get; set; }

       public virtual Company Company_CompanyId { get; set; }

       [Required]
       [DisplayName("Debit Ledger Account")]
       public int DebitLedgerAccountId { get; set; }

       public virtual LedgerAccount LedgerAccount_DebitLedgerAccountId { get; set; }

       [Required]
       [DisplayName("Debit Amount")]
       public Decimal DebitAmount { get; set; }

       [Required]
       [DisplayName("Credit Ledger Account")]
       public int CreditLedgerAccountId { get; set; }

       public virtual LedgerAccount LedgerAccount_CreditLedgerAccountId { get; set; }

       [Required]
       [DisplayName("Credit Amount")]
       public Decimal CreditAmount { get; set; }

       [Required]
       [DisplayName("Transaction Date")]
       [Column(TypeName = "datetime")]
       public DateTime TransactionDate { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Date Modied")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModied { get; set; }

       [DisplayName("Attachment")]
       [StringLength(100)] 
       public string Attachment { get; set; }


    }
}

