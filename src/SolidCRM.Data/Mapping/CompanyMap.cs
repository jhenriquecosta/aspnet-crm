using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class CompanyMap
    {
        public CompanyMap(EntityTypeBuilder<Company> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);
            tb.Property(o => o.About);
            tb.Property(o => o.Website).HasMaxLength(100);
            tb.Property(o => o.Phone).HasMaxLength(13);
            tb.Property(o => o.Fax).HasMaxLength(15);

        } 
    }
}
