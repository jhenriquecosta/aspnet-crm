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
    public class InvoiceItemController : BaseController
    {
        private readonly IInvoiceItemService _invoiceitemService;
        private readonly IInvoiceService _invoiceService;
        private readonly IQuantityUnitService _quantityunitService;

        public InvoiceItemController(IInvoiceItemService invoiceitemService,IInvoiceService invoiceService,IQuantityUnitService quantityunitService)
        {
            this._invoiceitemService = invoiceitemService;
            this._invoiceService = invoiceService;
            this._quantityunitService = quantityunitService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _invoiceitemService.GetAll().Select(m => new InvoiceItemViewModel { Id = m.Id,InvoiceId = m.Invoice_InvoiceId.To,Description = m.Description,Title = m.Title,Quantity = m.Quantity,UnitPrice = m.UnitPrice,QuantityUnitId = m.QuantityUnit_QuantityUnitId.Title,Total = m.Total,Tax = m.Tax,Discount = m.Discount,Adjustment = m.Adjustment }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<InvoiceItemViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<InvoiceItemViewModel> result = new DTResult<InvoiceItemViewModel>
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

        private IQueryable<InvoiceItemViewModel> FilterResult(string search, IQueryable<InvoiceItemViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<InvoiceItemViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.InvoiceId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Title.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.Quantity.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.UnitPrice.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.QuantityUnitId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.Total.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.Tax.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.Discount.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.Adjustment.ToString().ToLower().Contains(columnFilters[11].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            InvoiceItem model = new InvoiceItem();
            ViewBag.Invoices = new SelectList(_invoiceService.GetAll().ToArray(), "Id", "To");
            ViewBag.QuantityUnits = new SelectList(_quantityunitService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]InvoiceItem model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _invoiceitemService.Insert(model);
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
                InvoiceItem ObjInvoiceItem = _invoiceitemService.Get(id);
                ObjInvoiceItem.Id = _invoiceitemService.GetAll().Max(i=>i.Id)+1;
                ObjInvoiceItem.Description = "Copy_" + ObjInvoiceItem.Description;
                _invoiceitemService.Insert(ObjInvoiceItem);
                 
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
            InvoiceItem ObjInvoiceItem = _invoiceitemService.Get(id);
            ViewBag.Invoices = new SelectList(_invoiceService.GetAll().ToArray(), "Id", "To");
            ViewBag.QuantityUnits = new SelectList(_quantityunitService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjInvoiceItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InvoiceItem model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _invoiceitemService.Update(model);
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
                InvoiceItem ObjInvoiceItem = _invoiceitemService.Get(id); 
               _invoiceitemService.Delete(ObjInvoiceItem);

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

