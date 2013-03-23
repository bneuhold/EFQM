using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFQMWeb.Models;
using EFQMWeb.Common.DB;
using EFQMWeb.Common.Base;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using EFQMWeb.Common.Util;

namespace EFQMWeb.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST", Tip = "IZ" };
            }
            return View();
        }

        

        public ActionResult About()
        {
            return View();
        }
    }
}
