using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SolidCRM.Service;
using System.Linq.Dynamic.Core;
using SolidCRM.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SolidCRM.Web.Controllers
{
    public class MenuPermissionController : BaseController
    {
        private readonly IMenuPermissionService _menupermissionService;
        private readonly IMenuService _menuService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public MenuPermissionController(IMenuPermissionService menupermissionService,IMenuService menuService,IRoleService roleService,IUserService userService)
        {
            this._menupermissionService = menupermissionService;
            this._menuService = menuService;
            this._roleService = roleService;
            this._userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _menupermissionService.GetAll().Select(m => new MenuPermissionViewModel { Id = m.Id,MenuId = m.Menu_MenuId.MenuText,RoleId = m.Role_RoleId.RoleName,UserId = m.User_UserId.UserName,SortOrder = m.SortOrder,IsCreate = m.IsCreate,IsRead = m.IsRead,IsUpdate = m.IsUpdate,IsDelete = m.IsDelete }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<MenuPermissionViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<MenuPermissionViewModel> result = new DTResult<MenuPermissionViewModel>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private IQueryable<MenuPermissionViewModel> FilterResult(string search, IQueryable<MenuPermissionViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<MenuPermissionViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.MenuId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.RoleId.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.SortOrder.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.IsCreate.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.IsRead.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.IsUpdate.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.IsDelete.ToString().ToLower().Contains(columnFilters[9].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            MenuPermission model = new MenuPermission();
            ViewBag.Menus = new SelectList(_menuService.GetAll().ToArray(), "Id", "MenuText");
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]MenuPermission model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _menupermissionService.Insert(model);
                    sb.Append("Sumitted");

                    return Content(sb.ToString());
                }
                else
                {
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            sb.Append(err.ErrorMessage + "<br/>");
                        }
                    }
                    return Content(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString()); 
        }

        [HttpPost]
        public IActionResult Copy(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                MenuPermission ObjMenuPermission = _menupermissionService.Get(id);
                ObjMenuPermission.Id = _menupermissionService.GetAll().Max(i=>i.Id)+1;
                
                _menupermissionService.Insert(ObjMenuPermission);
                 
                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString()); 
        }

         
        public PartialViewResult Edit(int id)
        { 
            MenuPermission ObjMenuPermission = _menupermissionService.Get(id);
            ViewBag.Menus = new SelectList(_menuService.GetAll().ToArray(), "Id", "MenuText");
            ViewBag.Roles = new SelectList(_roleService.GetAll().ToArray(), "Id", "RoleName");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(ObjMenuPermission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MenuPermission model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _menupermissionService.Update(model);
                    sb.Append("Sumitted");
                    return Content(sb.ToString());
                }
                else
                {
                    foreach (var key in this.ViewData.ModelState.Keys)
                    {
                        foreach (var err in this.ViewData.ModelState[key].Errors)
                        {
                            sb.Append(err.ErrorMessage + "<br/>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }
             
            return Content(sb.ToString());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                MenuPermission ObjMenuPermission = _menupermissionService.Get(id); 
               _menupermissionService.Delete(ObjMenuPermission);

                sb.Append("Sumitted");
                return Content(sb.ToString());

            }
            catch (Exception ex)
            {
                sb.Append("Error :" + ex.Message);
            }

            return Content(sb.ToString());
        }

    }
}

