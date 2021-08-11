using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class LeadMap
    {
        public LeadMap(EntityTypeBuilder<Lead> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Name).HasMaxLength(100);
            tb.Property(o => o.Position).HasMaxLength(100);
            tb.Property(o => o.Email).HasMaxLength(22);
            tb.Property(o => o.Website).HasMaxLength(100);
            tb.Property(o => o.Phone).HasMaxLength(15);
            tb.Property(o => o.Company).HasMaxLength(100);
            tb.Property(o => o.Description).HasMaxLength(500);
            tb.HasOne(c => c.LeadStatus_LeadStatusId).WithMany(o => o.Lead_LeadStatusIds).HasForeignKey(o => o.LeadStatusId).IsRequired(true);
            tb.HasOne(c => c.Source_SourceId).WithMany(o => o.Lead_SourceIds).HasForeignKey(o => o.SourceId).IsRequired(true);
            tb.HasOne(c => c.User_AssignedToUserId).WithMany(o => o.Lead_AssignedToUserIds).HasForeignKey(o => o.AssignedToUserId).IsRequired(false);
            tb.Property(o => o.Address).HasMaxLength(500);
            tb.HasOne(c => c.Country_CountryId).WithMany(o => o.Lead_CountryIds).HasForeignKey(o => o.CountryId).IsRequired(true);

        } 
    }
}
