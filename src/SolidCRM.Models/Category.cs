using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class Category : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       [StringLength(120)] 
       public string Title { get; set; }

       [Required]
       [DisplayName("Details")]
       [StringLength(300)] 
       public string Details { get; set; }

       [DisplayName("Category Image")]
       [StringLength(200)] 
       public string CategoryImage { get; set; }

       [DisplayName("Parent")]
       public Nullable<int> ParentId { get; set; }

       public virtual Category Category2 { get; set; }

       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       public virtual ICollection<Category> Category_ParentIds { get; set; }

       public virtual ICollection<BlogCategory> BlogCategory_CategoryIds { get; set; }


    }
}

