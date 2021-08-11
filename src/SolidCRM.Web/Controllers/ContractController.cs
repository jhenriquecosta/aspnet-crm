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
    public class ContractController : BaseController
    {
        private readonly IContractService _contractService;
        private readonly ICompanyClientService _companyclientService;
        private readonly IContractTypeService _contracttypeService;
        private readonly IContractStatusService _contractstatusService;

        public ContractController(IContractService contractService,ICompanyClientService companyclientService,IContractTypeService contracttypeService,IContractStatusService contractstatusService)
        {
            this._contractService = contractService;
            this._companyclientService = companyclientService;
            this._contracttypeService = contracttypeService;
            this._contractstatusService = contractstatusService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _contractService.GetAll().Select(m => new ContractViewModel { Id = m.Id,Subject = m.Subject,ContractValue = m.ContractValue,StartDate = m.StartDate,EndDate = m.EndDate,Description = m.Description,CompanyClientId = m.CompanyClient_CompanyClientId.FirstName,ContractTypeId = m.ContractType_ContractTypeId.Name,ContractStatusId = m.ContractStatus_ContractStatusId.Name }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<ContractViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<ContractViewModel> result = new DTResult<ContractViewModel>
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

        private IQueryable<ContractViewModel> FilterResult(string search, IQueryable<ContractViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<ContractViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.Subject.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.ContractValue.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.StartDate.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.EndDate.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.CompanyClientId.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.ContractTypeId.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.ContractStatusId.ToString().ToLower().Contains(columnFilters[9].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Contract model = new Contract();
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.ContractTypes = new SelectList(_contracttypeService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ContractStatuss = new SelectList(_contractstatusService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Contract model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _contractService.Insert(model);
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
                Contract ObjContract = _contractService.Get(id);
                ObjContract.Id = _contractService.GetAll().Max(i=>i.Id)+1;
                ObjContract.Subject = "Copy_" + ObjContract.Subject;
                _contractService.Insert(ObjContract);
                 
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
            Contract ObjContract = _contractService.Get(id);
            ViewBag.CompanyClients = new SelectList(_companyclientService.GetAll().ToArray(), "Id", "FirstName");
            ViewBag.ContractTypes = new SelectList(_contracttypeService.GetAll().ToArray(), "Id", "Name");
            ViewBag.ContractStatuss = new SelectList(_contractstatusService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjContract);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contract model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _contractService.Update(model);
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
                Contract ObjContract = _contractService.Get(id); 
               _contractService.Delete(ObjContract);

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

