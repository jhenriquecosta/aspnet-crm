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
    public class ClientAddressController : BaseController
    {
        private readonly IClientAddressService _clientaddressService;
        private readonly IAddressTypeService _addresstypeService;
        private readonly ICountryService _countryService;

        public ClientAddressController(IClientAddressService clientaddressService,IAddressTypeService addresstypeService,ICountryService countryService)
        {
            this._clientaddressService = clientaddressService;
            this._addresstypeService = addresstypeService;
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
                var tableDataSource= _clientaddressService.GetAll().Select(m => new ClientAddressViewModel { Id = m.Id,AddressTypeId = m.AddressType_AddressTypeId.Name,Street = m.Street,City = m.City,State = m.State,ZipCode = m.ZipCode,CountryId = m.Country_CountryId.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ClientAddressViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ClientAddressViewModel> result = new DTResult<ClientAddressViewModel>
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

        private IQueryable<ClientAddressViewModel> FilterResult(string search, IQueryable<ClientAddressViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ClientAddressViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.AddressTypeId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Street.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.City.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.State.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ZipCode.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.CountryId.ToString().ToLower().Contains(columnFilters[7].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            ClientAddress model = new ClientAddress();
            ViewBag.AddressTypes = new SelectList(_addresstypeService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]ClientAddress model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _clientaddressService.Insert(model);
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
                ClientAddress ObjClientAddress = _clientaddressService.Get(id);
                ObjClientAddress.Id = _clientaddressService.GetAll().Max(i=>i.Id)+1;
                ObjClientAddress.Street = "Copy_" + ObjClientAddress.Street;
                _clientaddressService.Insert(ObjClientAddress);
                 
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
            ClientAddress ObjClientAddress = _clientaddressService.Get(id);
            ViewBag.AddressTypes = new SelectList(_addresstypeService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Countrys = new SelectList(_countryService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjClientAddress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ClientAddress model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _clientaddressService.Update(model);
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
                ClientAddress ObjClientAddress = _clientaddressService.Get(id); 
               _clientaddressService.Delete(ObjClientAddress);

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

