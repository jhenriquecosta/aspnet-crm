using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Blog : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(500)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Details")]
       public string Details { get; set; }

       [DisplayName("Added By")]
       public int? AddedBy { get; set; }

       [Required]
       [DisplayName("Date Added")]
       [Column(TypeName = "datetime")]
       public DateTime DateAdded { get; set; }

       [DisplayName("Modified By")]
       public int? ModifiedBy { get; set; }

       [DisplayName("Date Modied")]
       [Column(TypeName = "datetime")]
       public DateTime? DateModied { get; set; }

       [Required]
       [DisplayName("Is Published")]
       public bool IsPublished { get; set; }

       [DisplayName("View Count")]
       public int? ViewCount { get; set; }

       public virtual ICollection<BlogCategory> BlogCategory_BlogIds { get; set; }

       public virtual ICollection<NewsMedia> NewsMedia_BlogIds { get; set; }


    }
}

