using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class TicketTaskMap
    {
        public TicketTaskMap(EntityTypeBuilder<TicketTask> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Ticket_TicketId).WithMany(o => o.TicketTask_TicketIds).HasForeignKey(o => o.TicketId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.Property(o => o.TaskDetail).HasMaxLength(1000);
            tb.HasOne(c => c.Status_StatusId).WithMany(o => o.TicketTask_StatusIds).HasForeignKey(o => o.StatusId).IsRequired(true);
            tb.HasOne(c => c.Priority_PriorityId).WithMany(o => o.TicketTask_PriorityIds).HasForeignKey(o => o.PriorityId).IsRequired(true);

        } 
    }
}
