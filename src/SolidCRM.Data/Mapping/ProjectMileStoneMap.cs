using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ProjectMileStoneMap
    {
        public ProjectMileStoneMap(EntityTypeBuilder<ProjectMileStone> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Project_ProjectId).WithMany(o => o.ProjectMileStone_ProjectIds).HasForeignKey(o => o.ProjectId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.Property(o => o.Name).HasMaxLength(200);

        } 
    }
}
