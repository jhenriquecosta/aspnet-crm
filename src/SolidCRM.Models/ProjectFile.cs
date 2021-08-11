using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class ProjectFile : BaseEntity
    { 
       [Required]
       [DisplayName("Project")]
       public int ProjectId { get; set; }

       public virtual Project Project_ProjectId { get; set; }

       [Required]
       [DisplayName("File Manager")]
       public int FileManagerId { get; set; }

       public virtual FileManager FileManager_FileManagerId { get; set; }


    }
}

