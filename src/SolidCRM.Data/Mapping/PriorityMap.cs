using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class PriorityMap
    {
        public PriorityMap(EntityTypeBuilder<Priority> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(100);
            tb.Property(o => o.ColorName).HasMaxLength(100);

        } 
    }
}
