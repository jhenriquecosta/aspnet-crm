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
    public class FileManagerController : BaseController
    {
        private readonly IFileManagerService _filemanagerService;
        private readonly IUserService _userService;

        public FileManagerController(IFileManagerService filemanagerService,IUserService userService)
        {
            this._filemanagerService = filemanagerService;
            this._userService = userService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _filemanagerService.GetAll().Select(m => new FileManagerViewModel { Id = m.Id,Title = m.Title,Tags = m.Tags,UserId = m.User_UserId.UserName,FileUrl = m.FileUrl,FileExtension = m.FileExtension }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<FileManagerViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<FileManagerViewModel> result = new DTResult<FileManagerViewModel>
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

        private IQueryable<FileManagerViewModel> FilterResult(string search, IQueryable<FileManagerViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<FileManagerViewModel> results = dtResult;
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
                    results = results.Where(p => p.Tags.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.UserId.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.FileUrl.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.FileExtension.ToString().ToLower().Contains(columnFilters[6].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            FileManager model = new FileManager();
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]FileManager model,IFormFile fileUrl, string fileUrl5)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUrl != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileUrl.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            fileUrl.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.FileUrl = fileUrl.FileName;
                    }
                    else
                    {
                        model.FileUrl = fileUrl5;
                    }

                    _filemanagerService.Insert(model);
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
                FileManager ObjFileManager = _filemanagerService.Get(id);
                ObjFileManager.Id = _filemanagerService.GetAll().Max(i=>i.Id)+1;
                ObjFileManager.Title = "Copy_" + ObjFileManager.Title;
                _filemanagerService.Insert(ObjFileManager);
                 
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
            FileManager ObjFileManager = _filemanagerService.Get(id);
            ViewBag.Users = new SelectList(_userService.GetAll().ToArray(), "Id", "UserName");

            return PartialView(ObjFileManager);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FileManager model,IFormFile fileUrl, string fileUrl5)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (fileUrl != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileUrl.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            fileUrl.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.FileUrl = fileUrl.FileName;
                    }
                    else
                    {
                        model.FileUrl = fileUrl5;
                    }

                   _filemanagerService.Update(model);
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
                FileManager ObjFileManager = _filemanagerService.Get(id); 
               _filemanagerService.Delete(ObjFileManager);

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

