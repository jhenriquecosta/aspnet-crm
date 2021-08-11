using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class LedgerAccountMap
    {
        public LedgerAccountMap(EntityTypeBuilder<LedgerAccount> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(50);
            tb.Property(o => o.AccountCode).HasMaxLength(50);
            tb.Property(o => o.AccountColor).HasMaxLength(10);
            tb.HasOne(c => c.LedgerAccount2).WithMany(o => o.LedgerAccount_ParentIds).HasForeignKey(o => o.ParentId);
            tb.HasOne(c => c.Company_CompanyId).WithMany(o => o.LedgerAccount_CompanyIds).HasForeignKey(o => o.CompanyId).IsRequired(true);

        } 
    }
}
