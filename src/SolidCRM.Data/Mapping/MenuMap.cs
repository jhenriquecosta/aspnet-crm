using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class MenuMap
    {
        public MenuMap(EntityTypeBuilder<Menu> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.MenuText).HasMaxLength(100);
            tb.Property(o => o.MenuURL).HasMaxLength(400);
            tb.HasOne(c => c.Menu2).WithMany(o => o.Menu_ParentIds).HasForeignKey(o => o.ParentId);
            tb.Property(o => o.MenuIcon).HasMaxLength(100);

        } 
    }
}
