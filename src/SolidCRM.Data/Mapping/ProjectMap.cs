using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ProjectMap
    {
        public ProjectMap(EntityTypeBuilder<Project> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(200);
            tb.Property(o => o.Status);
            tb.HasOne(c => c.CompanyClient_CompanyClientId).WithMany(o => o.Project_CompanyClientIds).HasForeignKey(o => o.CompanyClientId).IsRequired(false);
            tb.Property(o => o.Details).HasMaxLength(2000);
            tb.HasOne(c => c.Status_StatusId).WithMany(o => o.Project_StatusIds).HasForeignKey(o => o.StatusId).IsRequired(true);
            tb.HasOne(c => c.Priority_PriorityId).WithMany(o => o.Project_PriorityIds).HasForeignKey(o => o.PriorityId).IsRequired(true);
            tb.Property(o => o.Tags).HasMaxLength(100);

        } 
    }
}
