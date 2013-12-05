using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using EFQMWeb.Common.Util;
using EFQMWeb.Common.Base;
using EFQMWeb.Models;

namespace EFQMWeb.Controllers
{
    public class TecajController : BaseController
    {
        //
        // GET: /Seminar/

        public ActionResult Index()
        {
            ViewBag.Title = "Online registracija";
            ViewBag.TecajList = Database.HuogTecajList(DateTime.Today);
            return View();
        }

        public ActionResult Finish()
        {
            return View();
        }

        //
        // GET: /Seminar/Details/5
        [HttpPost]
        public ActionResult Details(string oib)
        {
            if (string.IsNullOrEmpty(oib)) oib = null;
            DataTable table = Database.SeminarList(oib);
            PureJson result = new PureJson();
            if (table.Rows.Count == 0)
            {
                return new SimpleJsonResult();
            }

            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 0);
                using (SPJsonObject jO = new SPJsonObject(jRoot.GetWriter, "Data"))
                {
                    DataRow last = table.Rows[table.Rows.Count - 1];
                    foreach (DataColumn column in table.Columns)
                    {
                        if (column.ColumnName == "dateOfBirth" && !Utils.IsNull(last[column.ColumnName]))
                        {
                            DateTime born = (DateTime)last[column.ColumnName];
                            jRoot.Add(column.ColumnName, born.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            jRoot.Add(column.ColumnName, last[column.ColumnName]);
                        }
                    }
                }
            }
            return new SimpleJsonResult(result);
        }

        [HttpPost]
        public ActionResult Save(SeminarRegistration data)
        {
            PureJson result = new PureJson();
            if (string.IsNullOrEmpty(data.firstName))
            {
                using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                {
                    jRoot.Add("Status", 1);
                }
                return new SimpleJsonResult(result);
            }
            else if (string.IsNullOrEmpty(data.lastName))
            {
                using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                {
                    jRoot.Add("Status", 2);
                }
                return new SimpleJsonResult(result);
            }
            else if (string.IsNullOrEmpty(data.userOIB))
            {
                using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                {
                    jRoot.Add("Status", 3);
                }
                return new SimpleJsonResult(result);
            }
            Database.SeminarRegistrationSave(data);

            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 0);
            }
            return new SimpleJsonResult(result);
        }

        
    }
}
