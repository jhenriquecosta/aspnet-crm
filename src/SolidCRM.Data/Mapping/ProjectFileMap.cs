using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ProjectFileMap
    {
        public ProjectFileMap(EntityTypeBuilder<ProjectFile> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Project_ProjectId).WithMany(o => o.ProjectFile_ProjectIds).HasForeignKey(o => o.ProjectId).IsRequired(true);
            tb.HasOne(c => c.FileManager_FileManagerId).WithMany(o => o.ProjectFile_FileManagerIds).HasForeignKey(o => o.FileManagerId).IsRequired(true);

        } 
    }
}
