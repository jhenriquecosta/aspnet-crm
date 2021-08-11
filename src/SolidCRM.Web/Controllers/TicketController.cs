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
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly ICompanyClientService _companyclientService;
        private readonly IStatusService _statusService;
        private readonly IPriorityService _priorityService;
        private readonly ICompanyService _companyService;

        public TicketController(ITicketService ticketService,ICompanyClientService companyclientService,IStatusService statusService,IPriorityService priorityService,ICompanyService companyService)
        {
            this._ticketService = ticketService;
            this._companyclientService = companyclientService;
            this._statusService = statusService;
            this._priorityService = priorityService;
            this._companyService = companyService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _ticketService.GetAll().Select(m => new TicketViewModel { Id = m.Id,Title = m.Title,TicketDetail = m.TicketDetail,StartDate = m.StartDate,EndDate = m.EndDate,CompanyClientId = m.CompanyClient_CompanyClientId.FirstName,StatusId = m.Status_StatusId.Title,PriorityId = m.Priority_PriorityId.Title,CompanyId = m.Company_CompanyId.Name,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModied = m.DateModied,IsDelete = m.IsDelete }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<TicketViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<TicketViewModel> result = new DTResult<TicketViewModel>
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

        private IQueryable<TicketViewModel> FilterResult(string search, IQueryable<TicketViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<TicketViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Title.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.TicketDetail.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.StartDate.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.EndDate.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.CompanyClientId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.StatusId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.PriorityId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.CompanyId.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.DateModied.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.IsDelete.ToString().ToLower().Contains(columnFilters[14].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Ticket model = new Ticket();
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Ticket model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _ticketService.Insert(model);
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
                Ticket ObjTicket = _ticketService.Get(id);
                ObjTicket.Id = _ticketService.GetAll().Max(i=>i.Id)+1;
                ObjTicket.Title = "Copy_" + ObjTicket.Title;
                _ticketService.Insert(ObjTicket);
                 
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
            Ticket ObjTicket = _ticketService.Get(id);
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.Statuss = new SelectList(_statusService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Prioritys = new SelectList(_priorityService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjTicket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Ticket model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _ticketService.Update(model);
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
                Ticket ObjTicket = _ticketService.Get(id); 
               _ticketService.Delete(ObjTicket);

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

