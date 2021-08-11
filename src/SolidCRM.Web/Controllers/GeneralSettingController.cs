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
    public class GeneralSettingController : BaseController
    {
        private readonly IGeneralSettingService _generalsettingService;

        public GeneralSettingController(IGeneralSettingService generalsettingService)
        {
            this._generalsettingService = generalsettingService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _generalsettingService.GetAll().Select(m => new GeneralSettingViewModel { Id = m.Id,SettingKey = m.SettingKey,SettingValue = m.SettingValue,Description = m.Description,SettingGroup = m.SettingGroup }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<GeneralSettingViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<GeneralSettingViewModel> result = new DTResult<GeneralSettingViewModel>
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

        private IQueryable<GeneralSettingViewModel> FilterResult(string search, IQueryable<GeneralSettingViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<GeneralSettingViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.SettingKey.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.SettingValue.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.Description.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.SettingGroup.ToString().ToLower().Contains(columnFilters[5].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            GeneralSetting model = new GeneralSetting();

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]GeneralSetting model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    
                    _generalsettingService.Insert(model);
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
                GeneralSetting ObjGeneralSetting = _generalsettingService.Get(id);
                ObjGeneralSetting.Id = _generalsettingService.GetAll().Max(i=>i.Id)+1;
                ObjGeneralSetting.SettingKey = "Copy_" + ObjGeneralSetting.SettingKey;
                _generalsettingService.Insert(ObjGeneralSetting);
                 
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
            GeneralSetting ObjGeneralSetting = _generalsettingService.Get(id);

            return PartialView(ObjGeneralSetting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GeneralSetting model)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    
                   _generalsettingService.Update(model);
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
                GeneralSetting ObjGeneralSetting = _generalsettingService.Get(id); 
               _generalsettingService.Delete(ObjGeneralSetting);

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

