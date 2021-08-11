using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.UserName).HasMaxLength(100);
            tb.Property(o => o.Password).HasMaxLength(100);
            tb.Property(o => o.FirstName).HasMaxLength(100);
            tb.Property(o => o.LastName).HasMaxLength(100);
            tb.Property(o => o.ProfilePicture).HasMaxLength(200);
            tb.Property(o => o.Email).HasMaxLength(50);
            tb.Property(o => o.Facebook).HasMaxLength(100);
            tb.Property(o => o.LinkedIn).HasMaxLength(100);
            tb.Property(o => o.Skype).HasMaxLength(100);
            tb.Property(o => o.EmailSignature).HasMaxLength(500);
            tb.HasOne(c => c.Language_LanguageId).WithMany(o => o.User_LanguageIds).HasForeignKey(o => o.LanguageId).IsRequired(false);
            tb.HasOne(c => c.Direction_DirectionId).WithMany(o => o.User_DirectionIds).HasForeignKey(o => o.DirectionId).IsRequired(false);
            tb.Property(o => o.Phone).HasMaxLength(15);
            tb.Property(o => o.ChangePasswordCode).HasMaxLength(100);

        } 
    }
}
