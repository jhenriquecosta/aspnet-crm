using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class InvoiceItemMap
    {
        public InvoiceItemMap(EntityTypeBuilder<InvoiceItem> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Invoice_InvoiceId).WithMany(o => o.InvoiceItem_InvoiceIds).HasForeignKey(o => o.InvoiceId).IsRequired(true);
            tb.Property(o => o.Description).HasMaxLength(200);
            tb.Property(o => o.Title).HasMaxLength(50);
            tb.HasOne(c => c.QuantityUnit_QuantityUnitId).WithMany(o => o.InvoiceItem_QuantityUnitIds).HasForeignKey(o => o.QuantityUnitId).IsRequired(true);

        } 
    }
}
