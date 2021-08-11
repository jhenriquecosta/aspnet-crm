using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class FileManager : BaseEntity
    { 
       [Required]
       [DisplayName("Title")]
       public string Title { get; set; }

       [Required]
       [DisplayName("Tags")]
       public string Tags { get; set; }

       [Required]
       [DisplayName("User")]
       public int UserId { get; set; }

       public virtual User User_UserId { get; set; }

       [Required]
       [DisplayName("File Url")]
       [StringLength(1000)] 
       public string FileUrl { get; set; }

       [DisplayName("File Extension")]
       [StringLength(10)] 
       public string FileExtension { get; set; }

       public virtual ICollection<ProjectFile> ProjectFile_FileManagerIds { get; set; }


    }
}

