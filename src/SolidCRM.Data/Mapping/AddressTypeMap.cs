using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class AddressTypeMap
    {
        public AddressTypeMap(EntityTypeBuilder<AddressType> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);

        } 
    }
}
