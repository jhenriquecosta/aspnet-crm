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
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly ICompanyClientService _companyclientService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;

        public ProjectController(IProjectService projectService,ICompanyClientService companyclientService,IStatusService statusService,IPriorityService priorityService)
        {
            this._projectService = projectService;
            this._companyclientService = companyclientService;
            this._statusService = statusService;
            this._priorityService = priorityService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _projectService.GetAll().Select(m => new ProjectViewModel { Id = m.Id,Name = m.Name,AddedBy = m.AddedBy,StartDate = m.StartDate,EndDate = m.EndDate,CompanyOfficeId = m.CompanyOfficeId,Status = m.Status,CompanyClientId = m.CompanyClient_CompanyClientId.FirstName,Details = m.Details,TargetDate = m.TargetDate,DateAdded = m.DateAdded,DateModified = m.DateModified,ModifiedBy = m.ModifiedBy,StatusId = m.Status_StatusId.Title,PriorityId = m.Priority_PriorityId.Title,Tags = m.Tags,Amount = m.Amount }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ProjectViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ProjectViewModel> result = new DTResult<ProjectViewModel>
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

        private IQueryable<ProjectViewModel> FilterResult(string search, IQueryable<ProjectViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProjectViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Name.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.StartDate.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.EndDate.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.CompanyOfficeId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Status.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.CompanyClientId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.Details.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.TargetDate.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.StatusId.ToString().ToLower().Contains(columnFilters[14].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[15]))
                    results = results.Where(p => p.PriorityId.ToString().ToLower().Contains(columnFilters[15].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[16]))
                    results = results.Where(p => p.Tags.ToString().ToLower().Contains(columnFilters[16].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[17]))
                    results = results.Where(p => p.Amount.ToString().ToLower().Contains(columnFilters[17].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Project model = new Project();
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Project model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _projectService.Insert(model);
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
                Project ObjProject = _projectService.Get(id);
                ObjProject.Id = _projectService.GetAll().Max(i=>i.Id)+1;
                ObjProject.Name = "Copy_" + ObjProject.Name;
                _projectService.Insert(ObjProject);
                 
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
            Project ObjProject = _projectService.Get(id);
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjProject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _projectService.Update(model);
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
                Project ObjProject = _projectService.Get(id); 
               _projectService.Delete(ObjProject);

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

