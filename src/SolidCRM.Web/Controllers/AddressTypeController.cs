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
    public class AddressTypeController : BaseController
    {
        private readonly IAddressTypeService _addresstypeService;

        public AddressTypeController(IAddressTypeService addresstypeService)
        {
            this._addresstypeService = addresstypeService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _addresstypeService.GetAll().Select(m => new AddressTypeViewModel { Id = m.Id,Name = m.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<AddressTypeViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<AddressTypeViewModel> result = new DTResult<AddressTypeViewModel>
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

        private IQueryable<AddressTypeViewModel> FilterResult(string search, IQueryable<AddressTypeViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<AddressTypeViewModel> results = dtResult;
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

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            AddressType model = new AddressType();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]AddressType model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _addresstypeService.Insert(model);
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
                AddressType ObjAddressType = _addresstypeService.Get(id);
                ObjAddressType.Id = _addresstypeService.GetAll().Max(i=>i.Id)+1;
                ObjAddressType.Name = "Copy_" + ObjAddressType.Name;
                _addresstypeService.Insert(ObjAddressType);
                 
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
            AddressType ObjAddressType = _addresstypeService.Get(id);

            return PartialView(ObjAddressType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AddressType model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _addresstypeService.Update(model);
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
                AddressType ObjAddressType = _addresstypeService.Get(id); 
               _addresstypeService.Delete(ObjAddressType);

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

