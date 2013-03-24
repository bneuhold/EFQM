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
    [Authorize, UserInSessionAttribute]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserInfo()
        {
            return View();
        }
        

        public ActionResult About()
        {
            return View();
        }

        public ActionResult SaveUser(LoggedUser model)
        {
            PureJson result = new PureJson();
            LoggedUser user = Database.UserChange(model);
            if (user != null)
            {
                MySession.CurrentUser = user;
                using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                {
                    jRoot.Add("Status", 0);
                }
                return new SimpleJsonResult(result);
            }
            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 1);
            }
            return new SimpleJsonResult(result);
        }
    }
}
