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
    public class ProjectMemberController : BaseController
    {
        private readonly IProjectMemberService _projectmemberService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public ProjectMemberController(IProjectMemberService projectmemberService,IProjectService projectService,IUserService userService)
        {
            this._projectmemberService = projectmemberService;
            this._projectService = projectService;
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
                var tableDataSource= _projectmemberService.GetAll().Select(m => new ProjectMemberViewModel { Id = m.Id,ProjectId = m.Project_ProjectId.Name,UserId = m.User_UserId.UserName }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ProjectMemberViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ProjectMemberViewModel> result = new DTResult<ProjectMemberViewModel>
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

        private IQueryable<ProjectMemberViewModel> FilterResult(string search, IQueryable<ProjectMemberViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProjectMemberViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.ProjectId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[3].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            ProjectMember model = new ProjectMember();
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ProjectMember model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _projectmemberService.Insert(model);
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
                ProjectMember ObjProjectMember = _projectmemberService.Get(id);
                ObjProjectMember.Id = _projectmemberService.GetAll().Max(i=>i.Id)+1;
                
                _projectmemberService.Insert(ObjProjectMember);
                 
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
            ProjectMember ObjProjectMember = _projectmemberService.Get(id);
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(ObjProjectMember);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectMember model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _projectmemberService.Update(model);
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
                ProjectMember ObjProjectMember = _projectmemberService.Get(id); 
               _projectmemberService.Delete(ObjProjectMember);

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

