using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class PaymentModeMap
    {
        public PaymentModeMap(EntityTypeBuilder<PaymentMode> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(50);

        } 
    }
}
