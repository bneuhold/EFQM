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
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST" };
            }
            return View();
        }

        public ActionResult Upitnik(int? id)
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST" };
            }
            HuogUpitnikModel model = new HuogUpitnikModel();
            model.Pitanja = Database.HuogPitanjeList();
            model.Grupe = Database.HuogGrupaList();
            if (id.HasValue)
            {
                DataTable table = Database.HuogUpitnikList(MySession.CurrentUser.IdUser, id.Value);
                if (table.Rows.Count == 1)
                {
                    model.UpitnikId = id;
                }
            }
            
            return View(model);
        }

        public ActionResult Load(int UpitnikId)
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST" };
            }
            DataTable table = Database.HuogUpitnikList(MySession.CurrentUser.IdUser, UpitnikId);
            PureJson result = new PureJson();
            if (table.Rows.Count != 1)
            {
                return new SimpleJsonResult();
            }

            using (SPJsonObject jRoot = new SPJsonObject(new JsonKeyValueWriter(result.StringBuilder)))
            {
                jRoot.Add("Status", 0);
                jRoot.Add("Id", table.Rows[0]["Id"]);
                jRoot.Add("Naziv", table.Rows[0]["Naziv"]);
                jRoot.Add("Opis", table.Rows[0]["Opis"]);
                jRoot.Add("Datum", table.Rows[0]["Datum"]);

                table = Database.HuogUpitnikVrijednostiList(UpitnikId, null);
                using (SPJsonArray jA = new SPJsonArray(jRoot.GetWriter, "Vrijednosti"))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            if (!Utils.IsNull(row["Vrijednost" + i]))
                            {
                                using (SPJsonObject jO = new SPJsonObject(jA.GetWriter))
                                {
                                    jO.Add("A", i);
                                    jO.Add("P", row["PitanjeId"]);
                                    jO.Add("O", row["Oznaka"]);
                                    jO.Add("V", row["Vrijednost" + i]);
                                }
                            }
                        }
                    }
                }
                
            }

            return new SimpleJsonResult(result);
        }

        public ActionResult Save(string json)
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST" };
            }
            Upitnik upitnik = JsonConvert.DeserializeObject<Upitnik>(json);

            Hashtable h = new Hashtable();
            foreach (UpitnikVrijednostItem item in upitnik.Vrijednosti)
            {
                UpitnikVrijednostRezultat redak = null;
                if (!h.Contains(item.P))
                {
                    redak = new UpitnikVrijednostRezultat() { Id = item.ID, Oznaka = item.O, PitanjeId = item.P };
                    h[item.P] = redak;
                }
                else
                {
                    redak = (UpitnikVrijednostRezultat)h[item.P];
                }
                redak.Vrijednosti[item.A - 1] = item.V;
            }

            int id = DBOperations.Database.UpitnikSave(upitnik, h, MySession.CurrentUser.IdUser);

            return Json( new { Status = 0, UpitnikID = id});
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
