using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ProjectMemberMap
    {
        public ProjectMemberMap(EntityTypeBuilder<ProjectMember> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Project_ProjectId).WithMany(o => o.ProjectMember_ProjectIds).HasForeignKey(o => o.ProjectId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.ProjectMember_UserIds).HasForeignKey(o => o.UserId).IsRequired(true);

        } 
    }
}
