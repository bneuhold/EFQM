using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EFQMWeb.Common.Base;
using EFQMWeb.Common.Util;
using System.Collections;
using EFQMWeb.Models;
using System.Data;
using EFQMWeb.Common;

namespace EFQMWeb.Controllers
{
    public class IzvrsnostController : BaseController
    {
        //
        // GET: /Izvrsnost/

        public ActionResult Index()
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST", Tip = "IZ" };
            }
            return View(Database.HuogUpitnikList(MySession.CurrentUser.IdUser, null));
        }


        public ActionResult Upitnik(int? id)
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST", Tip = "IZ" };
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

        public ActionResult Result(int? id, string upitnikIDs)
        {
            UpitnikGraf result = new UpitnikGraf();
            using (IDataReader reader = Database.GetRezultat(id.Value, MySession.CurrentUser.Tip))
            {
                while (reader.Read())
                {
                    result.UpitnikId = id.Value;
                    result.Datum = (DateTime)reader["Datum"];
                    result.Naziv = reader["Naziv"].ToString();
                }
                reader.NextResult();
                int i = 0;
                while (reader.Read())
                {
                    result.BP[i++] = (int)reader["Vrijednost"];
                }
                reader.NextResult();
                i = 0;
                while (reader.Read())
                {
                    result.AV[i++] = (int)reader["Vrijednost"];
                }
                reader.NextResult();
                i = 0;
                while (reader.Read())
                {
                    result.QR[i++] = (int)reader["Vrijednost"];
                }
            }
            return View();
        }

        public ActionResult Load(int UpitnikId)
        {
            if (MySession.CurrentUser == null)
            {
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST", Tip = "IZ" };
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
                MySession.CurrentUser = new Common.Entity.LoggedUser() { IdUser = 1, Name = "TEST", Tip = "IZ" };
            }
            Upitnik upitnik = JsonConvert.DeserializeObject<Upitnik>(json);

            UpitnikRezultat result= Izracun.Process(upitnik.Prosjek);
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

            int id = Database.UpitnikSave(upitnik, h, MySession.CurrentUser.IdUser, result, MySession.CurrentUser.Tip);

            return Json(new { Status = 0, UpitnikId = id });
        }

    }
}
