using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class SourceMap
    {
        public SourceMap(EntityTypeBuilder<Source> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);

        } 
    }
}
