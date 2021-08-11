using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class InvoiceMap
    {
        public InvoiceMap(EntityTypeBuilder<Invoice> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.PaymentMode_PaymentModeId).WithMany(o => o.Invoice_PaymentModeIds).HasForeignKey(o => o.PaymentModeId).IsRequired(true);
            tb.Property(o => o.To).HasMaxLength(100);
            tb.Property(o => o.OtherInvoiceCode).HasMaxLength(50);
            tb.Property(o => o.Address).HasMaxLength(200);
            tb.HasOne(c => c.Company_CompanyId).WithMany(o => o.Invoice_CompanyIds).HasForeignKey(o => o.CompanyId).IsRequired(false);
            tb.HasOne(c => c.Country_CountryId).WithMany(o => o.Invoice_CountryIds).HasForeignKey(o => o.CountryId).IsRequired(true);
            tb.Property(o => o.ZipCode).HasMaxLength(10);
            tb.Property(o => o.Email).HasMaxLength(40);
            tb.Property(o => o.Mobile).HasMaxLength(15);
            tb.HasOne(c => c.ClientAddress_ClientAddressId).WithMany(o => o.Invoice_ClientAddressIds).HasForeignKey(o => o.ClientAddressId).IsRequired(false);

        } 
    }
}
