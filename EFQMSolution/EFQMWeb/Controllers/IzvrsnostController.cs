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
    [Authorize, UserInSessionAttribute]
    public class IzvrsnostController : BaseController
    {
        //
        // GET: /Izvrsnost/

        public ActionResult Index()
        {
            return View(Database.HuogUpitnikList(MySession.CurrentUser.Id, null));
        }


        public ActionResult Upitnik(int? id)
        {
            HuogUpitnikModel model = new HuogUpitnikModel();
            model.Pitanja = Database.HuogPitanjeList();
            model.Grupe = Database.HuogGrupaList();
            if (id.HasValue)
            {
                DataTable table = Database.HuogUpitnikList(MySession.CurrentUser.Id, id.Value);
                if (table.Rows.Count == 1)
                {
                    model.UpitnikId = id;
                }
            }

            return View(model);
        }

        public ActionResult Result(int? id, string upitnikIDs)
        {
            UpitnikGraf result = Izracun.Result(Database, id, null, MySession.CurrentUser.Type);
            /*using (IDataReader reader = Database.GetRezultat(id.Value, MySession.CurrentUser.Type))
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
                    result.BP[i++] = (int)Math.Round((decimal)reader["Vrijednost"],0);
                    result.SumBP += result.BP[i - 1];
                }
                reader.NextResult();
                i = 0;
                while (reader.Read())
                {
                    result.AV[i++] = (int)Math.Round((decimal)reader["Vrijednost"], 0);
                    result.SumAV += result.AV[i - 1];
                }
                reader.NextResult();
                i = 0;
                while (reader.Read())
                {
                    result.QR[i++] = (int)Math.Round((decimal)reader["Vrijednost"], 0);
                    result.SumQR += result.QR[i - 1];
                }
            }*/

            ViewBag.ContactLink = MyConfig.GetSetting("ContactLink");
            return View(result);
        }

        public ActionResult Load(int UpitnikId)
        {
            DataTable table = Database.HuogUpitnikList(MySession.CurrentUser.Id, UpitnikId);
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

            int id = Database.UpitnikSave(upitnik, h, MySession.CurrentUser.Id, result, MySession.CurrentUser.Type);

            return Json(new { Status = 0, UpitnikId = id });
        }

    }
}
