using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class FileManagerMap
    {
        public FileManagerMap(EntityTypeBuilder<FileManager> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title);
            tb.Property(o => o.Tags);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.FileManager_UserIds).HasForeignKey(o => o.UserId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.Property(o => o.FileUrl).HasMaxLength(1000);
            tb.Property(o => o.FileExtension).HasMaxLength(10);

        } 
    }
}
