using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ClientAddress : BaseEntity
    { 
       [DisplayName("Address Type")]
       public int? AddressTypeId { get; set; }

       public virtual AddressType AddressType_AddressTypeId { get; set; }

       [Required]
       [DisplayName("Street")]
       [StringLength(100)] 
       public string Street { get; set; }

       [Required]
       [DisplayName("City")]
       [StringLength(100)] 
       public string City { get; set; }

       [DisplayName("State")]
       [StringLength(100)] 
       public string State { get; set; }

       [DisplayName("Zip Code")]
       [StringLength(10)] 
       public string ZipCode { get; set; }

       [DisplayName("Country")]
       public int? CountryId { get; set; }

       public virtual Country Country_CountryId { get; set; }

       public virtual ICollection<Invoice> Invoice_ClientAddressIds { get; set; }


    }
}

