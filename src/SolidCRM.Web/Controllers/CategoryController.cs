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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _categoryService.GetAll().Select(m => new CategoryViewModel { Id = m.Id,Title = m.Title,Details = m.Details,CategoryImage = m.CategoryImage,ParentId = m.Category2.Title,IsActive = m.IsActive }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<CategoryViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<CategoryViewModel> result = new DTResult<CategoryViewModel>
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

        private IQueryable<CategoryViewModel> FilterResult(string search, IQueryable<CategoryViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<CategoryViewModel> results = dtResult;
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
                    results = results.Where(p => p.Details.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.CategoryImage.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.ParentId.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            Category model = new Category();
            ViewBag.Categorys = new SelectList(_categoryService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]Category model,IFormFile categoryImage, string categoryImage4)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    if (categoryImage != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", categoryImage.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            categoryImage.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.CategoryImage = categoryImage.FileName;
                    }
                    else
                    {
                        model.CategoryImage = categoryImage4;
                    }

                    _categoryService.Insert(model);
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
                Category ObjCategory = _categoryService.Get(id);
                ObjCategory.Id = _categoryService.GetAll().Max(i=>i.Id)+1;
                ObjCategory.Title = "Copy_" + ObjCategory.Title;
                _categoryService.Insert(ObjCategory);
                 
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
            Category ObjCategory = _categoryService.Get(id);
            ViewBag.Categorys = new SelectList(_categoryService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category model,IFormFile categoryImage, string categoryImage4)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (categoryImage != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", categoryImage.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            categoryImage.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.CategoryImage = categoryImage.FileName;
                    }
                    else
                    {
                        model.CategoryImage = categoryImage4;
                    }

                   _categoryService.Update(model);
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
                Category ObjCategory = _categoryService.Get(id); 
               _categoryService.Delete(ObjCategory);

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

