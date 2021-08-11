using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class CurrencyMap
    {
        public CurrencyMap(EntityTypeBuilder<Currency> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(30);
            tb.Property(o => o.Code).HasMaxLength(20);

        } 
    }
}
