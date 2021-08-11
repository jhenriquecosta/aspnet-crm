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
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPaymentModeService _paymentmodeService;
        private readonly ICompanyService _companyService;
        private readonly ICountryService _countryService;
        private readonly IClientAddressService _clientaddressService;

        public InvoiceController(IInvoiceService invoiceService,IPaymentModeService paymentmodeService,ICompanyService companyService,ICountryService countryService,IClientAddressService clientaddressService)
        {
            this._invoiceService = invoiceService;
            this._paymentmodeService = paymentmodeService;
            this._companyService = companyService;
            this._countryService = countryService;
            this._clientaddressService = clientaddressService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _invoiceService.GetAll().Select(m => new InvoiceViewModel { Id = m.Id,BillDate = m.BillDate,DueDate = m.DueDate,PaymentModeId = m.PaymentMode_PaymentModeId.Title,To = m.To,OtherInvoiceCode = m.OtherInvoiceCode,Address = m.Address,CreatedBy = m.CreatedBy,CompanyId = m.Company_CompanyId.Name,CountryId = m.Country_CountryId.Name,ZipCode = m.ZipCode,Email = m.Email,Mobile = m.Mobile,ClientAddressId = m.ClientAddress_ClientAddressId.Street }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<InvoiceViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<InvoiceViewModel> result = new DTResult<InvoiceViewModel>
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

        private IQueryable<InvoiceViewModel> FilterResult(string search, IQueryable<InvoiceViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<InvoiceViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.BillDate.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.DueDate.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.PaymentModeId.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.To.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.OtherInvoiceCode.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Address.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.CreatedBy.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.CompanyId.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.CountryId.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.ZipCode.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.Mobile.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.ClientAddressId.ToString().ToLower().Contains(columnFilters[14].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Invoice model = new Invoice();
            ViewBag.PaymentModes = new SelectList(_paymentmodeService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ClientAddresss = new SelectList(_clientaddressService.GetAll().ToArray(), "Id", "Street");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Invoice model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _invoiceService.Insert(model);
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
                Invoice ObjInvoice = _invoiceService.Get(id);
                ObjInvoice.Id = _invoiceService.GetAll().Max(i=>i.Id)+1;
                ObjInvoice.To = "Copy_" + ObjInvoice.To;
                _invoiceService.Insert(ObjInvoice);
                 
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
            Invoice ObjInvoice = _invoiceService.Get(id);
            ViewBag.PaymentModes = new SelectList(_paymentmodeService.GetAll().ToArray(), "Id", "Title");
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ClientAddresss = new SelectList(_clientaddressService.GetAll().ToArray(), "Id", "Street");

            return PartialView(ObjInvoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Invoice model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _invoiceService.Update(model);
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
                Invoice ObjInvoice = _invoiceService.Get(id); 
               _invoiceService.Delete(ObjInvoice);

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

