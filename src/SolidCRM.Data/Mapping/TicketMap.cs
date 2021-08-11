using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class TicketMap
    {
        public TicketMap(EntityTypeBuilder<Ticket> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(150);
            tb.Property(o => o.TicketDetail);
            tb.HasOne(c => c.CompanyClient_CompanyClientId).WithMany(o => o.Ticket_CompanyClientIds).HasForeignKey(o => o.CompanyClientId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.Status_StatusId).WithMany(o => o.Ticket_StatusIds).HasForeignKey(o => o.StatusId).IsRequired(true);
            tb.HasOne(c => c.Priority_PriorityId).WithMany(o => o.Ticket_PriorityIds).HasForeignKey(o => o.PriorityId).IsRequired(true);
            tb.HasOne(c => c.Company_CompanyId).WithMany(o => o.Ticket_CompanyIds).HasForeignKey(o => o.CompanyId).IsRequired(true);

        } 
    }
}
