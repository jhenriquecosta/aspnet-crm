using SolidCRM.Models; 
using System.Linq;
using SolidCRM.Repo;
using SolidCRM.Data;

namespace SolidCRM.Service
{
    public class MenuBarService : Repository<MenuPermission>, IMenuBarService
    {
        //private readonly IMenuService _MenuSer;
        //private readonly IMenuPermissionService _MenuPerSer;
        //public MenuBarService(IMenuService MenuSer, IMenuPermissionService MenuPerSer)
        //{
        //    this._MenuSer = MenuSer;
        //    this._MenuPerSer = MenuPerSer; 
        //}
        
        public MenuBarService(ApplicationContext dbContext) : base(dbContext) { }

        public MenuPermission[] GetMenuBarlist(int RoleId, int UserId)
        {
            return GetAllInclude(i=>i.Menu_MenuId).Where(i => (i.RoleId == RoleId && i.UserId == null) || i.UserId == UserId).ToArray();
        }

        public MenuPermission[] GetMenuBarlist()
        {
            return GetAllInclude(i => i.Menu_MenuId).ToArray();
        }

        

        
    }
}


