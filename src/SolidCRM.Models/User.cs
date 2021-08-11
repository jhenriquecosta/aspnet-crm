using System;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace SolidCRM.Models
{
    public class User : BaseEntity
    { 
       [Required]
       [DisplayName("User Name")]
       [StringLength(100)] 
       public string UserName { get; set; }

       [Required]
       [DisplayName("Password")]
       [StringLength(100)] 
       public string Password { get; set; }

       [Required]
       [DisplayName("First Name")]
       [StringLength(100)] 
       public string FirstName { get; set; }

       [DisplayName("Last Name")]
       [StringLength(100)] 
       public string LastName { get; set; }

       [DisplayName("Profile Picture")]
       [StringLength(200)] 
       public string ProfilePicture { get; set; }

       [DisplayName("Email")]
       [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
       [StringLength(50)] 

       public string Email { get; set; }

       [DisplayName("Facebook")]
       [StringLength(100)] 
       public string Facebook { get; set; }

       [DisplayName("Linked In")]
       [StringLength(100)] 
       public string LinkedIn { get; set; }

       [DisplayName("Skype")]
       [StringLength(100)] 
       public string Skype { get; set; }

       [DisplayName("Email Signature")]
       [StringLength(500)] 
       public string EmailSignature { get; set; }

       [DisplayName("Language")]
       public int? LanguageId { get; set; }

       public virtual Language Language_LanguageId { get; set; }

       [DisplayName("Direction")]
       public int? DirectionId { get; set; }

       public virtual Direction Direction_DirectionId { get; set; }

       [DisplayName("Phone")]
       [StringLength(15)] 
       public string Phone { get; set; }

       [DisplayName("Change Password Code")]
       [StringLength(100)] 
       public string ChangePasswordCode { get; set; }

       [Required]
       [DisplayName("Is Active")]
       public bool IsActive { get; set; }

       public virtual ICollection<RoleUser> RoleUser_UserIds { get; set; }

       public virtual ICollection<MenuPermission> MenuPermission_UserIds { get; set; }

       public virtual ICollection<Lead> Lead_AssignedToUserIds { get; set; }

       public virtual ICollection<ProjectMember> ProjectMember_UserIds { get; set; }

       public virtual ICollection<CompanyClient> CompanyClient_UserIds { get; set; }

       public virtual ICollection<FileManager> FileManager_UserIds { get; set; }

       public virtual ICollection<Todo> Todo_UserIds { get; set; }

       public virtual ICollection<Notes> Notes_AddedByUserIds { get; set; }


    }
}

