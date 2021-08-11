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
    public class LedgerAccountController : BaseController
    {
        private readonly ILedgerAccountService _ledgeraccountService;
        private readonly ICompanyService _companyService;

        public LedgerAccountController(ILedgerAccountService ledgeraccountService,ICompanyService companyService)
        {
            this._ledgeraccountService = ledgeraccountService;
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
                var tableDataSource= _ledgeraccountService.GetAll().Select(m => new LedgerAccountViewModel { Id = m.Id,Title = m.Title,CurrencyId = m.CurrencyId,AccountCode = m.AccountCode,AccountColor = m.AccountColor,ParentId = m.LedgerAccount2.Title,CompanyId = m.Company_CompanyId.Name,DateAdded = m.DateAdded,AddedBy = m.AddedBy }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<LedgerAccountViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<LedgerAccountViewModel> result = new DTResult<LedgerAccountViewModel>
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

        private IQueryable<LedgerAccountViewModel> FilterResult(string search, IQueryable<LedgerAccountViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<LedgerAccountViewModel> results = dtResult;
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
                    results = results.Where(p => p.CurrencyId.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.AccountCode.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.AccountColor.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.CompanyId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[9].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            LedgerAccount model = new LedgerAccount();
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]LedgerAccount model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _ledgeraccountService.Insert(model);
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
                LedgerAccount ObjLedgerAccount = _ledgeraccountService.Get(id);
                ObjLedgerAccount.Id = _ledgeraccountService.GetAll().Max(i=>i.Id)+1;
                ObjLedgerAccount.Title = "Copy_" + ObjLedgerAccount.Title;
                _ledgeraccountService.Insert(ObjLedgerAccount);
                 
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
            LedgerAccount ObjLedgerAccount = _ledgeraccountService.Get(id);
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjLedgerAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(LedgerAccount model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _ledgeraccountService.Update(model);
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
                LedgerAccount ObjLedgerAccount = _ledgeraccountService.Get(id); 
               _ledgeraccountService.Delete(ObjLedgerAccount);

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

