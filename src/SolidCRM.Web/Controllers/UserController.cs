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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;
        private readonly IDirectionService _directionService;

        public UserController(IUserService userService,ILanguageService languageService,IDirectionService directionService)
        {
            this._userService = userService;
            this._languageService = languageService;
            this._directionService = directionService;

        }

        public IActionResult Index()
        {
            return View();
        }
 
        public IActionResult GetGrid([FromBody]DTParameters param)
        {
            try
            {
                var tableDataSource= _userService.GetAll().Select(m => new UserViewModel { Id = m.Id,UserName = m.UserName,Password = m.Password,FirstName = m.FirstName,LastName = m.LastName,ProfilePicture = m.ProfilePicture,Email = m.Email,Facebook = m.Facebook,LinkedIn = m.LinkedIn,Skype = m.Skype,EmailSignature = m.EmailSignature,LanguageId = m.Language_LanguageId.Name,DirectionId = m.Direction_DirectionId.Name,Phone = m.Phone,ChangePasswordCode = m.ChangePasswordCode,IsActive = m.IsActive }).AsQueryable();

                List<String> columnSearch = new List<string>();

                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }

                var filterdData= FilterResult(param.Search.Value, tableDataSource, columnSearch, param.SearchFromLength);
                List<UserViewModel> data = filterdData.OrderBy(param.SortOrder).Skip(param.Start).Take(param.Length).ToList();
                int count = filterdData.Count();

                DTResult<UserViewModel> result = new DTResult<UserViewModel>
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

        private IQueryable<UserViewModel> FilterResult(string search, IQueryable<UserViewModel> dtResult, List<string> columnFilters, int searchTake = 500)
        {
            IQueryable<UserViewModel> results = dtResult;
            if (searchTake == 0)
                results = results.OrderByDescending(i => i.Id).AsQueryable();
            else
                results = results.OrderByDescending(i => i.Id).Take(searchTake).AsQueryable();

            if (!columnFilters.All(x => string.IsNullOrWhiteSpace(x)))
            {
                if (!string.IsNullOrEmpty(columnFilters[1]))
                    results = results.Where(p => p.Id.ToString().ToLower().Contains(columnFilters[1].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[2]))
                    results = results.Where(p => p.UserName.ToString().ToLower().Contains(columnFilters[2].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[3]))
                    results = results.Where(p => p.Password.ToString().ToLower().Contains(columnFilters[3].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[4]))
                    results = results.Where(p => p.FirstName.ToString().ToLower().Contains(columnFilters[4].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[5]))
                    results = results.Where(p => p.LastName.ToString().ToLower().Contains(columnFilters[5].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[6]))
                    results = results.Where(p => p.ProfilePicture.ToString().ToLower().Contains(columnFilters[6].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[7]))
                    results = results.Where(p => p.Email.ToString().ToLower().Contains(columnFilters[7].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[8]))
                    results = results.Where(p => p.Facebook.ToString().ToLower().Contains(columnFilters[8].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[9]))
                    results = results.Where(p => p.LinkedIn.ToString().ToLower().Contains(columnFilters[9].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[10]))
                    results = results.Where(p => p.Skype.ToString().ToLower().Contains(columnFilters[10].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[11]))
                    results = results.Where(p => p.EmailSignature.ToString().ToLower().Contains(columnFilters[11].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[12]))
                    results = results.Where(p => p.LanguageId.ToString().ToLower().Contains(columnFilters[12].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[13]))
                    results = results.Where(p => p.DirectionId.ToString().ToLower().Contains(columnFilters[13].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[14]))
                    results = results.Where(p => p.Phone.ToString().ToLower().Contains(columnFilters[14].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[15]))
                    results = results.Where(p => p.ChangePasswordCode.ToString().ToLower().Contains(columnFilters[15].ToLower()));
                if (!string.IsNullOrEmpty(columnFilters[16]))
                    results = results.Where(p => p.IsActive.ToString().ToLower().Contains(columnFilters[16].ToLower()));

            }
            return results.AsQueryable();
        }


        [HttpGet]
        public PartialViewResult Create()
        {
            User model = new User();
            ViewBag.Languages = new SelectList(_languageService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Directions = new SelectList(_directionService.GetAll().ToArray(), "Id", "Name");

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Create([FromForm]User model,IFormFile profilePicture, string profilePicture6)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                if (ModelState.IsValid)
                {
                    if (profilePicture != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", profilePicture.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            profilePicture.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.ProfilePicture = profilePicture.FileName;
                    }
                    else
                    {
                        model.ProfilePicture = profilePicture6;
                    }

                    _userService.Insert(model);
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
                User ObjUser = _userService.Get(id);
                ObjUser.Id = _userService.GetAll().Max(i=>i.Id)+1;
                ObjUser.UserName = "Copy_" + ObjUser.UserName;
                _userService.Insert(ObjUser);
                 
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
            User ObjUser = _userService.Get(id);
            ViewBag.Languages = new SelectList(_languageService.GetAll().ToArray(), "Id", "Name");
            ViewBag.Directions = new SelectList(_directionService.GetAll().ToArray(), "Id", "Name");

            return PartialView(ObjUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User model,IFormFile profilePicture, string profilePicture6)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(); 
            try
            {
                if (ModelState.IsValid)
                {
                    if (profilePicture != null)
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", profilePicture.FileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            profilePicture.CopyTo(stream);
                        }
                        ModelState.Clear();
                        model.ProfilePicture = profilePicture.FileName;
                    }
                    else
                    {
                        model.ProfilePicture = profilePicture6;
                    }

                   _userService.Update(model);
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
                User ObjUser = _userService.Get(id); 
               _userService.Delete(ObjUser);

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

