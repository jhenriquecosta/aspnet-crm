using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class AppSetting : BaseEntity
    { 
       [Required]
       [DisplayName("App Name")]
       [StringLength(100)] 
       public string AppName { get; set; }

       [DisplayName("App Short Name")]
       [StringLength(50)] 
       public string AppShortName { get; set; }

       [DisplayName("App Version")]
       [StringLength(15)] 
       public string AppVersion { get; set; }

       [Required]
       [DisplayName("Is Toggle Sidebar")]
       public bool IsToggleSidebar { get; set; }

       [Required]
       [DisplayName("Is Boxed Layout")]
       public bool IsBoxedLayout { get; set; }

       [Required]
       [DisplayName("Is Fixed Layout")]
       public bool IsFixedLayout { get; set; }

       [Required]
       [DisplayName("Is Toggle Right Sidebar")]
       public bool IsToggleRightSidebar { get; set; }

       [DisplayName("Skin")]
       [StringLength(20)] 
       public string Skin { get; set; }

       [DisplayName("Footer Text")]
       [StringLength(150)] 
       public string FooterText { get; set; }


    }
}

