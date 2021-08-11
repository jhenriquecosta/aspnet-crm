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
    public class CompanyClientController : BaseController
    {
        private readonly ICompanyClientService _companyclientService;
        private readonly ICompanyService _companyService;
        private readonly ICountryService _countryService;
        private readonly IUserService _userService;
        private readonly ICurrencyService _currencyService;

        public CompanyClientController(ICompanyClientService companyclientService,ICompanyService companyService,ICountryService countryService,IUserService userService,ICurrencyService currencyService)
        {
            this._companyclientService = companyclientService;
            this._companyService = companyService;
            this._countryService = countryService;
            this._userService = userService;
            this._currencyService = currencyService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _companyclientService.GetAll().Select(m => new CompanyClientViewModel { Id = m.Id,CompanyId = m.Company_CompanyId.Name,FirstName = m.FirstName,LastName = m.LastName,IsActive = m.IsActive,Email = m.Email,Phone = m.Phone,VATNumber = m.VATNumber,Latitude = m.Latitude,Longitude = m.Longitude,Address = m.Address,CountryId = m.Country_CountryId.Name,UserId = m.User_UserId.UserName,CurrencyId = m.Currency_CurrencyId.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<CompanyClientViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<CompanyClientViewModel> result = new DTResult<CompanyClientViewModel>
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

        private IQueryable<CompanyClientViewModel> FilterResult(string search, IQueryable<CompanyClientViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<CompanyClientViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.CompanyId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.FirstName.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.LastName.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Phone.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.VATNumber.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.Latitude.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.Longitude.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.Address.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.CountryId.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.CurrencyId.ToString().ToLower().Contains(columnFilters[14].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            CompanyClient model = new CompanyClient();
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Currencys = new SelectList(_currencyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]CompanyClient model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _companyclientService.Insert(model);
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
                CompanyClient ObjCompanyClient = _companyclientService.Get(id);
                ObjCompanyClient.Id = _companyclientService.GetAll().Max(i=>i.Id)+1;
                ObjCompanyClient.FirstName = "Copy_" + ObjCompanyClient.FirstName;
                _companyclientService.Insert(ObjCompanyClient);
                 
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
            CompanyClient ObjCompanyClient = _companyclientService.Get(id);
            ViewBag.Companys = new SelectList(_companyService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");
            ViewBag.Currencys = new SelectList(_currencyService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjCompanyClient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CompanyClient model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _companyclientService.Update(model);
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
                CompanyClient ObjCompanyClient = _companyclientService.Get(id); 
               _companyclientService.Delete(ObjCompanyClient);

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

