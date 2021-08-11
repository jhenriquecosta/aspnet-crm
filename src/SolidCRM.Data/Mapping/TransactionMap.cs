using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class TransactionMap
    {
        public TransactionMap(EntityTypeBuilder<Transaction> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(200);
            tb.HasOne(c => c.Company_CompanyId).WithMany(o => o.Transaction_CompanyIds).HasForeignKey(o => o.CompanyId).IsRequired(true);
            tb.HasOne(c => c.LedgerAccount_DebitLedgerAccountId).WithMany(o => o.Transaction_DebitLedgerAccountIds).HasForeignKey(o => o.DebitLedgerAccountId).IsRequired(true);
            tb.HasOne(c => c.LedgerAccount_CreditLedgerAccountId).WithMany(o => o.Transaction_CreditLedgerAccountIds).HasForeignKey(o => o.CreditLedgerAccountId).IsRequired(true);
            tb.Property(o => o.Attachment).HasMaxLength(100);

        } 
    }
}
