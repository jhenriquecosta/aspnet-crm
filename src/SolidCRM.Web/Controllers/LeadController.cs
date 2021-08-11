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
    public class LeadController : BaseController
    {
        private readonly ILeadService _leadService;
        private readonly ILeadStatusService _leadstatusService;
        private readonly ISourceService _sourceService;
        private readonly IUserService _userService;
        private readonly ICountryService _countryService;

        public LeadController(ILeadService leadService,ILeadStatusService leadstatusService,ISourceService sourceService,IUserService userService,ICountryService countryService)
        {
            this._leadService = leadService;
            this._leadstatusService = leadstatusService;
            this._sourceService = sourceService;
            this._userService = userService;
            this._countryService = countryService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _leadService.GetAll().Select(m => new LeadViewModel { Id = m.Id,Name = m.Name,Position = m.Position,Email = m.Email,Website = m.Website,Phone = m.Phone,Company = m.Company,Description = m.Description,LeadStatusId = m.LeadStatus_LeadStatusId.Name,SourceId = m.Source_SourceId.Name,AssignedToUserId = m.User_AssignedToUserId.UserName,OnDated = m.OnDated,Address = m.Address,CountryId = m.Country_CountryId.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<LeadViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<LeadViewModel> result = new DTResult<LeadViewModel>
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

        private IQueryable<LeadViewModel> FilterResult(string search, IQueryable<LeadViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<LeadViewModel> results = dtResult;
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
                    results = results.Where(p => p.Position.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.Website.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.Phone.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Company.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.LeadStatusId.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.SourceId.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.AssignedToUserId.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.OnDated.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.Address.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.CountryId.ToString().ToLower().Contains(columnFilters[14].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Lead model = new Lead();
            ViewBag.LeadStatuss = new SelectList(_leadstatusService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Sources = new SelectList(_sourceService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Lead model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _leadService.Insert(model);
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
                Lead ObjLead = _leadService.Get(id);
                ObjLead.Id = _leadService.GetAll().Max(i=>i.Id)+1;
                ObjLead.Name = "Copy_" + ObjLead.Name;
                _leadService.Insert(ObjLead);
                 
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
            Lead ObjLead = _leadService.Get(id);
            ViewBag.LeadStatuss = new SelectList(_leadstatusService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Sources = new SelectList(_sourceService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjLead);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Lead model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _leadService.Update(model);
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
                Lead ObjLead = _leadService.Get(id); 
               _leadService.Delete(ObjLead);

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

