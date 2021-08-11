using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SolidCRM.Web
{
    [Authorize]
    public class BaseController : Controller
    {  

    }
     
     
}
