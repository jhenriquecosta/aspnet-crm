using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class LeadStatusMap
    {
        public LeadStatusMap(EntityTypeBuilder<LeadStatus> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);

        } 
    }
}
