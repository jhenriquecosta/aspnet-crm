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
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _roleService.GetAll().Select(m => new RoleViewModel { Id = m.Id,RoleName = m.RoleName,IsActive = m.IsActive }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<RoleViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<RoleViewModel> result = new DTResult<RoleViewModel>
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

        private IQueryable<RoleViewModel> FilterResult(string search, IQueryable<RoleViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<RoleViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.RoleName.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[3].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Role model = new Role();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Role model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _roleService.Insert(model);
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
                Role ObjRole = _roleService.Get(id);
                ObjRole.Id = _roleService.GetAll().Max(i=>i.Id)+1;
                ObjRole.RoleName = "Copy_" + ObjRole.RoleName;
                _roleService.Insert(ObjRole);
                 
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
            Role ObjRole = _roleService.Get(id);

            return PartialView(ObjRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Role model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _roleService.Update(model);
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
                Role ObjRole = _roleService.Get(id); 
               _roleService.Delete(ObjRole);

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

