using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFQMWeb.Common.Base;
using System.Data;
using EFQMWeb.Common.Util;
using EFQMWeb.Models;

namespace EFQMWeb.Controllers
{
    [AdminAccess]
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SeminarList()
        {
            ViewBag.TecajList = Database.HuogTecajList(DateTime.Today);
            return View();
        }

        public ActionResult SeminarDetails(int id)
        {
            PureJson result = new PureJson();
            DataTable data = Database.HuogTecajList(DateTime.Today);
            DataRow current = null;
            foreach (DataRow row in data.Rows)
            {
                if ((int)row["Id"] == id)
                    current = row;
            }
            if (current != null)
            {
                using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
                {
                    jRoot.Add("Status", 0);
                    using (SPJsonObject jO = new SPJsonObject(jRoot.GetWriter, "Data"))
                    {
                        jO.Add("SeminarId", current["Id"]);
                        jO.Add("Title", current["Title"]);
                        jRoot.Add("DateFrom", Utils.ToISODateString(current["DateFrom"]));
                        jRoot.Add("DateTo", Utils.ToISODateString(current["DateTo"]));
                        jO.Add("Active", current["Active"]);
                        jO.Add("Description", current["Description"]);
                    }
                }
                return new SimpleJsonResult(result);
            }
            else
            {
                return new SimpleJsonResult();
            }
        }

        [HttpPost]
        public ActionResult SeminarSave(Seminar input)
        {
            PureJson result = new PureJson();
            Database.SeminarSave(input);
            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 0);
                using (SPJsonObject jO = new SPJsonObject(jRoot.GetWriter, "Data"))
                {
                    jO.Add("SeminarId", input.SeminarId);
                    jO.Add("Title", input.Title);
                    jRoot.Add("DateFrom", input.DateFrom);
                    jRoot.Add("DateTo", input.DateTo);
                    jO.Add("Active", input.Active);
                    jO.Add("Description", input.Description);
                }
            }
            return new SimpleJsonResult(result);
        }

        //
        // GET: /Admin/Details/5
        [HttpGet]
        public ActionResult SeminarUserList()
        {
            ViewBag.TecajList = Database.HuogTecajList(DateTime.Today);
            return View();
        }

        [HttpPost]
        public ActionResult SeminarUserList(int? SeminarId, string DateFrom, string DateTo)
        {
            PureJson result = new PureJson();
            DataTable table = Database.SeminarRegistrationSearch(SeminarId, DateFrom, DateTo);
            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 0);
                using (SPJsonArray jA = new SPJsonArray(jRoot.GetWriter, "List"))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        using (SPJsonObject jO =new SPJsonObject(jRoot.GetWriter))
                        {
                            foreach (DataColumn column in table.Columns)
                            {
                                if (column.ColumnName == "dateOfBirth" && !Utils.IsNull(row[column.ColumnName]))
                                {
                                    DateTime born = (DateTime)row[column.ColumnName];
                                    jRoot.Add(column.ColumnName, Utils.ToISODateString(born));
                                }
                                else if (column.ColumnName == "dateCreated" && !Utils.IsNull(row[column.ColumnName]))
                                {
                                    DateTime dt = (DateTime)row[column.ColumnName];
                                    jRoot.Add(column.ColumnName, Utils.ToISODateString(dt));
                                }
                                else
                                {
                                    jRoot.Add(column.ColumnName, row[column.ColumnName]);
                                }
                            }
                        }
                    }
                }
            }
            return new SimpleJsonResult(result);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
