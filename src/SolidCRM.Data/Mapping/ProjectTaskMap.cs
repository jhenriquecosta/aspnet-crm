using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ProjectTaskMap
    {
        public ProjectTaskMap(EntityTypeBuilder<ProjectTask> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Project_ProjectId).WithMany(o => o.ProjectTask_ProjectIds).HasForeignKey(o => o.ProjectId).IsRequired(true);
            tb.Property(o => o.TaskNames).HasMaxLength(100);
            tb.Property(o => o.Description);
            tb.HasOne(c => c.Status_StatusId).WithMany(o => o.ProjectTask_StatusIds).HasForeignKey(o => o.StatusId).IsRequired(true);
            tb.HasOne(c => c.Priority_PriorityId).WithMany(o => o.ProjectTask_PriorityIds).HasForeignKey(o => o.PriorityId).IsRequired(true);
            tb.HasOne(c => c.ProjectTask2).WithMany(o => o.ProjectTask_ParentIds).HasForeignKey(o => o.ParentId);
            tb.HasOne(c => c.ProjectMileStone_ProjectMileStoneId).WithMany(o => o.ProjectTask_ProjectMileStoneIds).HasForeignKey(o => o.ProjectMileStoneId).IsRequired(false);

        } 
    }
}
