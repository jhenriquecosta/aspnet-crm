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
    public class ProjectMileStoneController : BaseController
    {
        private readonly IProjectMileStoneService _projectmilestoneService;
        private readonly IProjectService _projectService;

        public ProjectMileStoneController(IProjectMileStoneService projectmilestoneService,IProjectService projectService)
        {
            this._projectmilestoneService = projectmilestoneService;
            this._projectService = projectService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _projectmilestoneService.GetAll().Select(m => new ProjectMileStoneViewModel { Id = m.Id,ProjectId = m.Project_ProjectId.Name,Name = m.Name,Amount = m.Amount }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ProjectMileStoneViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ProjectMileStoneViewModel> result = new DTResult<ProjectMileStoneViewModel>
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

        private IQueryable<ProjectMileStoneViewModel> FilterResult(string search, IQueryable<ProjectMileStoneViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProjectMileStoneViewModel> results = dtResult;
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
                    results = results.Where(p => p.Name.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Amount.ToString().ToLower().Contains(columnFilters[4].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            ProjectMileStone model = new ProjectMileStone();
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ProjectMileStone model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _projectmilestoneService.Insert(model);
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
                ProjectMileStone ObjProjectMileStone = _projectmilestoneService.Get(id);
                ObjProjectMileStone.Id = _projectmilestoneService.GetAll().Max(i=>i.Id)+1;
                ObjProjectMileStone.Name = "Copy_" + ObjProjectMileStone.Name;
                _projectmilestoneService.Insert(ObjProjectMileStone);
                 
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
            ProjectMileStone ObjProjectMileStone = _projectmilestoneService.Get(id);
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjProjectMileStone);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectMileStone model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _projectmilestoneService.Update(model);
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
                ProjectMileStone ObjProjectMileStone = _projectmilestoneService.Get(id); 
               _projectmilestoneService.Delete(ObjProjectMileStone);

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

