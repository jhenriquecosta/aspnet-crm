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
    public class ProjectTaskController : BaseController
    {
        private readonly IProjectTaskService _projecttaskService;
        private readonly IProjectService _projectService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly IProjectMileStoneService _projectmilestoneService;

        public ProjectTaskController(IProjectTaskService projecttaskService,IProjectService projectService,IStatusService statusService,IPriorityService priorityService,IProjectMileStoneService projectmilestoneService)
        {
            this._projecttaskService = projecttaskService;
            this._projectService = projectService;
            this._statusService = statusService;
            this._priorityService = priorityService;
            this._projectmilestoneService = projectmilestoneService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _projecttaskService.GetAll().Select(m => new ProjectTaskViewModel { Id = m.Id,ProjectId = m.Project_ProjectId.Name,TaskNames = m.TaskNames,Description = m.Description,UserId = m.UserId,StartDate = m.StartDate,EndDate = m.EndDate,DateAdded = m.DateAdded,DateModified = m.DateModified,AddedBy = m.AddedBy,ModifiedBy = m.ModifiedBy,StatusId = m.Status_StatusId.Title,PriorityId = m.Priority_PriorityId.Title,ParentId = m.ProjectTask2.TaskNames,ProjectMileStoneId = m.ProjectMileStone_ProjectMileStoneId.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ProjectTaskViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ProjectTaskViewModel> result = new DTResult<ProjectTaskViewModel>
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

        private IQueryable<ProjectTaskViewModel> FilterResult(string search, IQueryable<ProjectTaskViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ProjectTaskViewModel> results = dtResult;
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
                    results = results.Where(p => p.TaskNames.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.StartDate.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.EndDate.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.DateModified.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.StatusId.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.PriorityId.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[14].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[15]))
                    results = results.Where(p => p.ProjectMileStoneId.ToString().ToLower().Contains(columnFilters[15].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            ProjectTask model = new ProjectTask();
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");
            ViewBag.ProjectTasks = new SelectList(_projecttaskService.GetAll().ToArray(), "Id", "TaskNames");
            ViewBag.ProjectMileStones = new SelectList(_projectmilestoneService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ProjectTask model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _projecttaskService.Insert(model);
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
                ProjectTask ObjProjectTask = _projecttaskService.Get(id);
                ObjProjectTask.Id = _projecttaskService.GetAll().Max(i=>i.Id)+1;
                ObjProjectTask.TaskNames = "Copy_" + ObjProjectTask.TaskNames;
                _projecttaskService.Insert(ObjProjectTask);
                 
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
            ProjectTask ObjProjectTask = _projecttaskService.Get(id);
            ViewBag.Projects = new SelectList(_projectService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");
            ViewBag.ProjectTasks = new SelectList(_projecttaskService.GetAll().ToArray(), "Id", "TaskNames");
            ViewBag.ProjectMileStones = new SelectList(_projectmilestoneService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjProjectTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectTask model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _projecttaskService.Update(model);
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
                ProjectTask ObjProjectTask = _projecttaskService.Get(id); 
               _projecttaskService.Delete(ObjProjectTask);

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

