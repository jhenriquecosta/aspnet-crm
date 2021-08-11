using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Notes : BaseEntity
    { 
       [Required]
       [DisplayName("Note")]
       [StringLength(1000)] 
       public string Note { get; set; }

       [DisplayName("Added By User")]
       public int? AddedByUserId { get; set; }

       public virtual User User_AddedByUserId { get; set; }

       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime? DateAdded { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Date Modied")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModied { get; set; }


    }
}

