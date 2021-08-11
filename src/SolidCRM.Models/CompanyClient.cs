using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class CompanyClient : BaseEntity
    { 
       [Required]
       [DisplayName("Company")]
       public int CompanyId { get; set; }

       public virtual Company Company_CompanyId { get; set; }

       [Required]
       [DisplayName("First Name")]
       [StringLength(50)] 
       public string FirstName { get; set; }

       [DisplayName("Last Name")]
       [StringLength(50)] 
       public string LastName { get; set; }

       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       [DisplayName("Email")]
       [StringLength(50)] 
       public string Email { get; set; }

       [DisplayName("Phone")]
       [StringLength(15)] 
       public string Phone { get; set; }

       [DisplayName("V A T Number")]
       [StringLength(50)] 
       public string VATNumber { get; set; }

       [DisplayName("Latitude")]
       [StringLength(50)] 
       public string Latitude { get; set; }

       [DisplayName("Longitude")]
       [StringLength(100)] 
       public string Longitude { get; set; }

       [DisplayName("Address")]
       [StringLength(100)] 
       public string Address { get; set; }

       [DisplayName("Country")]
       public int? CountryId { get; set; }

       public virtual Country Country_CountryId { get; set; }

       [DisplayName("User")]
       public int? UserId { get; set; }

       public virtual User User_UserId { get; set; }

       [Required]
       [DisplayName("Currency")]
       public int CurrencyId { get; set; }

       public virtual Currency Currency_CurrencyId { get; set; }

       public virtual ICollection<Contract> Contract_CompanyClientIds { get; set; }

       public virtual ICollection<Ticket> Ticket_CompanyClientIds { get; set; }

       public virtual ICollection<Project> Project_CompanyClientIds { get; set; }


    }
}

