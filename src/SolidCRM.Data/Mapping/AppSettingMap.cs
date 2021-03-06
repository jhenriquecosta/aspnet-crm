using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class AppSettingMap
    {
        public AppSettingMap(EntityTypeBuilder<AppSetting> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.AppName).HasMaxLength(100);
            tb.Property(o => o.AppShortName).HasMaxLength(50);
            tb.Property(o => o.AppVersion).HasMaxLength(15);
            tb.Property(o => o.Skin).HasMaxLength(20);
            tb.Property(o => o.FooterText).HasMaxLength(150);

        } 
    }
}
