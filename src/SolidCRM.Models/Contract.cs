using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Contract : BaseEntity
    { 
       [Required]
       [DisplayName("Subject")]
       [StringLength(100)] 
       public string Subject { get; set; }

       [Required]
       [DisplayName("Contract Value")]
       public Decimal ContractValue { get; set; }

       [Required]
       [DisplayName("Start Date")]
       [Column(TypeName = "datetime")]
       public DateTime StartDate { get; set; }

       [DisplayName("End Date")]
       [Column(TypeName = "datetime")]
       public DateTime? EndDate { get; set; }

       [DisplayName("Description")]
       [StringLength(500)] 
       public string Description { get; set; }

       [Required]
       [DisplayName("Company Client")]
       public int CompanyClientId { get; set; }

       public virtual CompanyClient CompanyClient_CompanyClientId { get; set; }

       [Required]
       [DisplayName("Contract Type")]
       public int ContractTypeId { get; set; }

       public virtual ContractType ContractType_ContractTypeId { get; set; }

       [Required]
       [DisplayName("Contract Status")]
       public int ContractStatusId { get; set; }

       public virtual ContractStatus ContractStatus_ContractStatusId { get; set; }


    }
}

