using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class CategoryMap
    {
        public CategoryMap(EntityTypeBuilder<Category> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(120);
            tb.Property(o => o.Details).HasMaxLength(300);
            tb.Property(o => o.CategoryImage).HasMaxLength(200);
            tb.HasOne(c => c.Category2).WithMany(o => o.Category_ParentIds).HasForeignKey(o => o.ParentId);

        } 
    }
}
