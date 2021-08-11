using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class NewsMediaMap
    {
        public NewsMediaMap(EntityTypeBuilder<NewsMedia> tb)
        {
            tb.HasKey(o => o.Id);
            tb.HasOne(c => c.Blog_BlogId).WithMany(o => o.NewsMedia_BlogIds).HasForeignKey(o => o.BlogId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            tb.Property(o => o.MediaFile).HasMaxLength(100);

        } 
    }
}
