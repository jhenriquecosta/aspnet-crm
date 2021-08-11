using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class CompanyClientMap
    {
        public CompanyClientMap(EntityTypeBuilder<CompanyClient> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Company_CompanyId).WithMany(o => o.CompanyClient_CompanyIds).HasForeignKey(o => o.CompanyId).IsRequired(true);
            tb.Property(o => o.FirstName).HasMaxLength(50);
            tb.Property(o => o.LastName).HasMaxLength(50);
            tb.Property(o => o.Email).HasMaxLength(50);
            tb.Property(o => o.Phone).HasMaxLength(15);
            tb.Property(o => o.VATNumber).HasMaxLength(50);
            tb.Property(o => o.Latitude).HasMaxLength(50);
            tb.Property(o => o.Longitude).HasMaxLength(100);
            tb.Property(o => o.Address).HasMaxLength(100);
            tb.HasOne(c => c.Country_CountryId).WithMany(o => o.CompanyClient_CountryIds).HasForeignKey(o => o.CountryId).IsRequired(false);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.CompanyClient_UserIds).HasForeignKey(o => o.UserId).IsRequired(false);
            tb.HasOne(c => c.Currency_CurrencyId).WithMany(o => o.CompanyClient_CurrencyIds).HasForeignKey(o => o.CurrencyId).IsRequired(true);

        } 
    }
}
