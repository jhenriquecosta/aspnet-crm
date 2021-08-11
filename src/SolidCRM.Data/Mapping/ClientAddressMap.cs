using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ClientAddressMap
    {
        public ClientAddressMap(EntityTypeBuilder<ClientAddress> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.AddressType_AddressTypeId).WithMany(o => o.ClientAddress_AddressTypeIds).HasForeignKey(o => o.AddressTypeId).IsRequired(false);
            tb.Property(o => o.Street).HasMaxLength(100);
            tb.Property(o => o.City).HasMaxLength(100);
            tb.Property(o => o.State).HasMaxLength(100);
            tb.Property(o => o.ZipCode).HasMaxLength(10);
            tb.HasOne(c => c.Country_CountryId).WithMany(o => o.ClientAddress_CountryIds).HasForeignKey(o => o.CountryId).IsRequired(false);

        } 
    }
}
