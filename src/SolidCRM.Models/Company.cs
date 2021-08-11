using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Company : BaseEntity
    { 
       [Required]
       [DisplayName("Name")]
       [StringLength(100)] 
       public string Name { get; set; }

       [Required]
       [DisplayName("About")]
       public string About { get; set; }

       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       [DisplayName("Website")]
       [StringLength(100)] 
       public string Website { get; set; }

       [DisplayName("Phone")]
       [StringLength(13)] 
       public string Phone { get; set; }

       [DisplayName("Fax")]
       [StringLength(15)] 
       public string Fax { get; set; }

       public virtual ICollection<Ticket> Ticket_CompanyIds { get; set; }

       public virtual ICollection<Invoice> Invoice_CompanyIds { get; set; }

       public virtual ICollection<Transaction> Transaction_CompanyIds { get; set; }

       public virtual ICollection<LedgerAccount> LedgerAccount_CompanyIds { get; set; }

       public virtual ICollection<CompanyClient> CompanyClient_CompanyIds { get; set; }


    }
}

