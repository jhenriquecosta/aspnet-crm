using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class BlogCategoryMap
    {
        public BlogCategoryMap(EntityTypeBuilder<BlogCategory> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Blog_BlogId).WithMany(o => o.BlogCategory_BlogIds).HasForeignKey(o => o.BlogId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.HasOne(c => c.Category_CategoryId).WithMany(o => o.BlogCategory_CategoryIds).HasForeignKey(o => o.CategoryId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);

        } 
    }
}
