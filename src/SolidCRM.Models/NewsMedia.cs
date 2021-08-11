using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class NewsMedia : BaseEntity
    { 
       [Required]
       [DisplayName("Blog")]
       public int BlogId { get; set; }

       public virtual Blog Blog_BlogId { get; set; }

       [DisplayName("Media File")]
       [StringLength(100)] 
       public string MediaFile { get; set; }

       [DisplayName("Display Sort Order")]
       public int? DisplaySortOrder { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

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

