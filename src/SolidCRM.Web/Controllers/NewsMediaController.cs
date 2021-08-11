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
    public class NewsMediaController : BaseController
    {
        private readonly INewsMediaService _newsmediaService;
        private readonly IBlogService _blogService;

        public NewsMediaController(INewsMediaService newsmediaService,IBlogService blogService)
        {
            this._newsmediaService = newsmediaService;
            this._blogService = blogService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _newsmediaService.GetAll().Select(m => new NewsMediaViewModel { Id = m.Id,BlogId = m.Blog_BlogId.Title,MediaFile = m.MediaFile,DisplaySortOrder = m.DisplaySortOrder,AddedBy = m.AddedBy,DateAdded = m.DateAdded,ModifiedBy = m.ModifiedBy,DateModied = m.DateModied }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<NewsMediaViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<NewsMediaViewModel> result = new DTResult<NewsMediaViewModel>
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

        private IQueryable<NewsMediaViewModel> FilterResult(string search, IQueryable<NewsMediaViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<NewsMediaViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.BlogId.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.MediaFile.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.DisplaySortOrder.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.AddedBy.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.DateAdded.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.ModifiedBy.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.DateModied.ToString().ToLower().Contains(columnFilters[8].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            NewsMedia model = new NewsMedia();
            ViewBag.Blogs = new SelectList(_blogService.GetAll().ToArray(), "Id", "Title");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]NewsMedia model,IFormFile mediaFile, string mediaFile3)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    if (mediaFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", mediaFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            mediaFile.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.MediaFile = mediaFile.FileName;
                    }
                    else
                    {
                        model.MediaFile = mediaFile3;
                    }

                    _newsmediaService.Insert(model);
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
                NewsMedia ObjNewsMedia = _newsmediaService.Get(id);
                ObjNewsMedia.Id = _newsmediaService.GetAll().Max(i=>i.Id)+1;
                ObjNewsMedia.MediaFile = "Copy_" + ObjNewsMedia.MediaFile;
                _newsmediaService.Insert(ObjNewsMedia);
                 
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
            NewsMedia ObjNewsMedia = _newsmediaService.Get(id);
            ViewBag.Blogs = new SelectList(_blogService.GetAll().ToArray(), "Id", "Title");

            return PartialView(ObjNewsMedia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NewsMedia model,IFormFile mediaFile, string mediaFile3)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (mediaFile != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", mediaFile.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            mediaFile.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.MediaFile = mediaFile.FileName;
                    }
                    else
                    {
                        model.MediaFile = mediaFile3;
                    }

                   _newsmediaService.Update(model);
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
                NewsMedia ObjNewsMedia = _newsmediaService.Get(id); 
               _newsmediaService.Delete(ObjNewsMedia);

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

