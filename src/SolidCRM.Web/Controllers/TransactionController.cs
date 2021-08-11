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
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly ICompanyService _companyService;
        private readonly ILedgerAccountService _ledgeraccountService; 

        public TransactionController(ITransactionService transactionService,ICompanyService companyService,ILedgerAccountService ledgeraccountService )
        {
            this._transactionService = transactionService;
            this._companyService = companyService;
            this._ledgeraccountService = ledgeraccountService; 

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _transactionService.GetAll().Select(m => new TransactionViewModel { Id = m.Id,Title = m.Title,DateAdded = m.DateAdded,AddedBy = m.AddedBy,CompanyId = m.Company_CompanyId.Name,DebitLedgerAccountId = m.LedgerAccount_DebitLedgerAccountId.Title,DebitAmount = m.DebitAmount,CreditLedgerAccountId = m.LedgerAccount_CreditLedgerAccountId.Title,CreditAmount = m.CreditAmount,TransactionDate = m.TransactionDate,ModifiedBy = m.ModifiedBy,DateModied = m.DateModied,Attachment = m.Attachment }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<TransactionViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<TransactionViewModel> result = new DTResult<TransactionViewModel>
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

        private IQueryable<TransactionViewModel> FilterResult(string search, IQueryable<TransactionViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<TransactionViewModel> results = dtResult;
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
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.CompanyId.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.DebitLedgerAccountId.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.DebitAmount.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.CreditLedgerAccountId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.CreditAmount.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.TransactionDate.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.DateModied.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.Attachment.ToString().ToLower().Contains(columnFilters[13].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Transaction model = new Transaction();
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Transaction model,IFormFile attachment, string attachment13)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", attachment.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            attachment.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.Attachment = attachment.FileName;
                    }
                    else
                    {
                        model.Attachment = attachment13;
                    }

                    _transactionService.Insert(model);
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
                Transaction ObjTransaction = _transactionService.Get(id);
                ObjTransaction.Id = _transactionService.GetAll().Max(i=>i.Id)+1;
                ObjTransaction.Title = "Copy_" + ObjTransaction.Title;
                _transactionService.Insert(ObjTransaction);
                 
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
            Transaction ObjTransaction = _transactionService.Get(id);
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");
            ViewBag.LedgerAccounts = new SelectList(_ledgeraccountService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjTransaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction model,IFormFile attachment, string attachment13)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachment != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", attachment.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            attachment.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.Attachment = attachment.FileName;
                    }
                    else
                    {
                        model.Attachment = attachment13;
                    }

                   _transactionService.Update(model);
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
                Transaction ObjTransaction = _transactionService.Get(id); 
               _transactionService.Delete(ObjTransaction);

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

