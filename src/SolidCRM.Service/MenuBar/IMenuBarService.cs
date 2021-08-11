using SolidCRM.Models; 
using SolidCRM.Repo;

namespace SolidCRM.Service
{
    public interface IMenuBarService : IRepository<MenuPermission>
    {
        MenuPermission[] GetMenuBarlist(int RoleId,int UserId);
        MenuPermission[] GetMenuBarlist();
    }
}


