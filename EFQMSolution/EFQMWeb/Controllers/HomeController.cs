using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFQMWeb.Models;
using EFQMWeb.Common.DB;
using EFQMWeb.Common.Base;

namespace EFQMWeb.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult Upitnik()
        {
            HuogUpitnikModel model = new HuogUpitnikModel();
            model.Pitanja = Database.HuogPitanjeList();
            model.Grupe = Database.HuogGrupaList();
            return View(model);
        }

        public ActionResult Save(string json)
        {

        }

        public ActionResult About()
        {
            return View();
        }
    }
}
