using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class NotesMap
    {
        public NotesMap(EntityTypeBuilder<Notes> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Note).HasMaxLength(1000);
            tb.HasOne(c => c.User_AddedByUserId).WithMany(o => o.Notes_AddedByUserIds).HasForeignKey(o => o.AddedByUserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);

        } 
    }
}
