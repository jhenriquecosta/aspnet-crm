using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class ContractMap
    {
        public ContractMap(EntityTypeBuilder<Contract> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Subject).HasMaxLength(100);
            tb.Property(o => o.Description).HasMaxLength(500);
            tb.HasOne(c => c.CompanyClient_CompanyClientId).WithMany(o => o.Contract_CompanyClientIds).HasForeignKey(o => o.CompanyClientId).IsRequired(true);
            tb.HasOne(c => c.ContractType_ContractTypeId).WithMany(o => o.Contract_ContractTypeIds).HasForeignKey(o => o.ContractTypeId).IsRequired(true);
            tb.HasOne(c => c.ContractStatus_ContractStatusId).WithMany(o => o.Contract_ContractStatusIds).HasForeignKey(o => o.ContractStatusId).IsRequired(true);

        } 
    }
}
