using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFQMWeb.Models;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EFQMWeb.Common.DB;

namespace EFQMWeb.Common
{
    public class Izracun
    {

        public static UpitnikRezultat Process(IList<UpitnikProsjek> input)
        {
            UpitnikRezultatItem[] result = new UpitnikRezultatItem[9];
            decimal[] sum = new decimal[9];
            foreach (UpitnikProsjek item in input)
            {
                if (item.G <= 5)
                {
                    sum[item.G - 1] += item.V;
                }
                else if ((item.G == 6) || (item.G == 7))
                {
                    if (item.O == "6a" || item.O == "7a")
                    {
                        sum[item.G - 1] += item.V * (decimal)0.75;
                    }
                    else
                    {
                        sum[item.G - 1] += item.V * (decimal)0.25;
                    }
                }
                else if (item.G == 8)
                {
                    if (item.O == "8a")
                    {
                        sum[item.G - 1] += item.V * (decimal)0.25;
                    }
                    else
                    {
                        sum[item.G - 1] += item.V * (decimal)0.75;
                    }
                }
                else if (item.G == 9    )
                {
                    sum[item.G - 1] += item.V * (decimal)0.5;
                }
            }
            result[0] = new UpitnikRezultatItem() {Grupa=1, Vrijednost = (int) Math.Round(((decimal)sum[0]/5),0) };
            result[1] = new UpitnikRezultatItem() { Grupa = 2, Vrijednost = (int)Math.Round(((decimal)sum[1] / 4), 0) };
            result[2] = new UpitnikRezultatItem() { Grupa = 3, Vrijednost = (int)Math.Round(((decimal)sum[2] / 5), 0) };
            result[3] = new UpitnikRezultatItem() { Grupa = 4, Vrijednost = (int)Math.Round(((decimal)sum[3] / 5), 0) };
            result[4] = new UpitnikRezultatItem() { Grupa = 5, Vrijednost = (int)Math.Round(((decimal)sum[4] / 5), 0) };

            result[5] = new UpitnikRezultatItem() { Grupa = 6, Vrijednost = (int)Math.Round((decimal)sum[5], 0) };
            result[6] = new UpitnikRezultatItem() { Grupa = 7, Vrijednost = (int)Math.Round((decimal)sum[6], 0) };
            result[7] = new UpitnikRezultatItem() { Grupa = 8, Vrijednost = (int)Math.Round((decimal)sum[7], 0) };
            result[8] = new UpitnikRezultatItem() { Grupa = 9, Vrijednost = (int)Math.Round((decimal)sum[8], 0) };

            int s = 0;
            s += (int)Math.Round(result[0].Vrijednost * (decimal)1.0, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)0.8, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)0.9, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)0.9, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)1.4, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)2.0, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)0.9, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)0.6, 0);
            s += (int)Math.Round(result[0].Vrijednost * (decimal)1.5, 0);

            return new UpitnikRezultat() {Grupe = result, Ocjena = s };

        }

        public static UpitnikGraf Result(DBOperations Database, int? id, string uuid, string userType)
        {
            UpitnikGraf result = new UpitnikGraf();
            using (IDataReader reader = Database.GetRezultat(id, uuid, userType))
            {
                while (reader.Read())
                {
                    result.UpitnikId = (int)reader["Id"];
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
            }
            return result;
        }
    }
}