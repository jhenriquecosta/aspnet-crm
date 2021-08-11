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
    public class TicketTaskController : BaseController
    {
        private readonly ITicketTaskService _tickettaskService;
        private readonly ITicketService _ticketService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;

        public TicketTaskController(ITicketTaskService tickettaskService,ITicketService ticketService,IStatusService statusService,IPriorityService priorityService)
        {
            this._tickettaskService = tickettaskService;
            this._ticketService = ticketService;
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
                var tableDataSource= _tickettaskService.GetAll().Select(m => new TicketTaskViewModel { Id = m.Id,TicketId = m.Ticket_TicketId.Title,StartDate = m.StartDate,EndDate = m.EndDate,TaskDetail = m.TaskDetail,StatusId = m.Status_StatusId.Title,PriorityId = m.Priority_PriorityId.Title,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModied = m.DateModied,IsDelete = m.IsDelete,TakeTimeInHour = m.TakeTimeInHour }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<TicketTaskViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<TicketTaskViewModel> result = new DTResult<TicketTaskViewModel>
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

        private IQueryable<TicketTaskViewModel> FilterResult(string search, IQueryable<TicketTaskViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<TicketTaskViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.TicketId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.StartDate.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.EndDate.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.TaskDetail.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.StatusId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.PriorityId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.DateModied.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.IsDelete.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.TakeTimeInHour.ToString().ToLower().Contains(columnFilters[13].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            TicketTask model = new TicketTask();
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]TicketTask model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _tickettaskService.Insert(model);
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
                TicketTask ObjTicketTask = _tickettaskService.Get(id);
                ObjTicketTask.Id = _tickettaskService.GetAll().Max(i=>i.Id)+1;
                ObjTicketTask.TaskDetail = "Copy_" + ObjTicketTask.TaskDetail;
                _tickettaskService.Insert(ObjTicketTask);
                 
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
            TicketTask ObjTicketTask = _tickettaskService.Get(id);
            ViewBag.Tickets = new SelectList(_ticketService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjTicketTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TicketTask model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _tickettaskService.Update(model);
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
                TicketTask ObjTicketTask = _tickettaskService.Get(id); 
               _tickettaskService.Delete(ObjTicketTask);

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

