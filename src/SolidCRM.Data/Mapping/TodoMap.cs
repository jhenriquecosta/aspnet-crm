using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolidCRM.Models;  
using Microsoft.EntityFrameworkCore;
namespace SolidCRM.Data
{
    public class TodoMap
    {
        public TodoMap(EntityTypeBuilder<Todo> tb)
        {
            tb.HasKey(o => o.Id);
            tb.Property(o => o.Description).HasMaxLength(200);
            tb.HasOne(c => c.User_UserId).WithMany(o => o.Todo_UserIds).HasForeignKey(o => o.UserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);

        } 
    }
}
