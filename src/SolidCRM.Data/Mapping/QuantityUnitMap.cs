using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class QuantityUnitMap
    {
        public QuantityUnitMap(EntityTypeBuilder<QuantityUnit> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title);

        } 
    }
}
