using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Todo : BaseEntity
    { 
       [Required]
       [DisplayName("Description")]
       [StringLength(200)] 
       public string Description { get; set; }

       [DisplayName("User")]
       public int? UserId { get; set; }

       public virtual User User_UserId { get; set; }

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

