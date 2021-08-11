using SolidCRM.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using SolidCRM.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace SolidCRM.Web
{  
    public class MenuBarViewComponent : ViewComponent
    {
        private readonly IMenuBarService _MenuBarSer; 
        private readonly IMemoryCache _cache;

        public MenuBarViewComponent(IMenuBarService MenuBarSer , IMemoryCache cache)
        {
            this._MenuBarSer = MenuBarSer; 
            this._cache = cache;
        }

        //public IViewComponentResult Invoke()
        //{
        //    //var articles = _articleService.GetNewArticles(numberOfItems);
        //    //return View(articles);
        //    return View();
        //}


        public HtmlString Invoke(IEnumerable<Claim> Claim)
        {
            StringBuilder sb = new StringBuilder();
            int? ParentId = null;

             //get role id and role regarding to role bind this
            var UserId = Convert.ToInt32(Env.GetUserInfo("Id", Claim));
            var RoleId = Convert.ToInt32(Env.GetUserInfo("RoleId", Claim));

            //var cacheItemKey = "jApMenuBar" + userId + "Us" + RoleId;
            var cacheItemKey = "AllMenuBar";
             
            var globle = (List<MenuPermission>)_cache.Get(cacheItemKey);
           
            if (globle == null)
            {
                globle = _MenuBarSer.GetMenuBarlist().ToList();
                //listMenuPer = (List<MenuPermission>)globle;
                _cache.Set(cacheItemKey, globle, DateTime.Now.AddMinutes(60));
            }
            
            sb.Append("<ul class=\"sidebar-menu\">");
            sb.Append("<li class=\"active\"> <a href=\"/Home\"> <i class=\"fa fa-dashboard\"></i> <span>Dashboard</span> </a> </li>");

            sb.Append(GetMenuBar(ParentId, globle.Where(i => (i.RoleId == RoleId && i.UserId == null) || i.UserId == UserId).ToArray()));
            sb.Append("</ul>"); 
            return new HtmlString(sb.ToString());
        }




        private HtmlString GetMenuBar(int? ParentId, MenuPermission[] q)
        {
            StringBuilder sb = new StringBuilder();
            if (q != null)
            {
                foreach (var item in q.Where(i => i.Menu_MenuId.ParentId == ParentId).OrderBy(i => i.SortOrder))
                {
                    var js = q;

                    if (js.Count(j => j.Menu_MenuId.ParentId == item.Menu_MenuId.Id) > 0)
                    {
                        if (item.Menu_MenuId.ParentId == null)
                        {
                            sb.Append("<li class=\"treeview\"> <a href=\"#\"> " + item.Menu_MenuId.MenuIcon + " <span>" + item.Menu_MenuId.MenuText + "</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">");
                        }
                        else
                        {
                            sb.Append("<li class=\"treeview\"> <a href=\"#\"> " + item.Menu_MenuId.MenuIcon + " <span>" + item.Menu_MenuId.MenuText + "</span> <i class=\"fa fa-angle-left pull-right\"></i>  </a><ul class=\"treeview-menu\">");
                        }
                        sb.Append(GetMenuBar(item.Menu_MenuId.Id, q));
                    }
                    else
                    {
                        if (item.Menu_MenuId.ParentId == null)
                        {
                            sb.Append("<li class=\"\"> <a href=\"/" + item.Menu_MenuId.MenuURL + "\"> " + item.Menu_MenuId.MenuIcon + " <span>" + item.Menu_MenuId.MenuText + "</span></a></li>");
                        }
                        else
                        {
                            sb.Append("<li class=\"\"> <a href=\"/" + item.Menu_MenuId.MenuURL + "\"> " + item.Menu_MenuId.MenuIcon + " <span>" + item.Menu_MenuId.MenuText + "</span></a></li>");
                        } 
                    }

                }
                sb.Append("</ul>");
            }
             
            return new HtmlString(sb.ToString());
        }

    }
}

