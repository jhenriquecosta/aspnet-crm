using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class BlogMap
    {
        public BlogMap(EntityTypeBuilder<Blog> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Title).HasMaxLength(500);
            tb.Property(o => o.Details);

        } 
    }
}
