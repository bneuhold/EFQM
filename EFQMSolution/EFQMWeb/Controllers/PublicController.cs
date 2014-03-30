using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFQMWeb.Common.Base;
using EFQMWeb.Models;
using EFQMWeb.Common;
using EFQMWeb.Common.Util;

namespace EFQMWeb.Controllers
{
    public class PublicController : BaseController
    {
        //
        // GET: /Public/

        public ActionResult IzvrsnostRezultat(string qpid, string utype)
        {
            UpitnikGraf result = Izracun.Result(Database, null, qpid, utype);
            ViewBag.ContactLink = MyConfig.GetSetting("ContactLink");
            return View("IzvrsnostResult", result);
        }
    }
}
