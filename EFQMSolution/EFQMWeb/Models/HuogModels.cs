using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace EFQMWeb.Models
{
    public class HuogModels
    {
    }

    public class HuogUpitnikModel
    {
        public DataTable Pitanja { get; set; }
        public DataTable Grupe { get; set; }
        public int? UpitnikId { get; set; }
    }

    public class UpitnikVrijednostItem
    {
        public int P { get; set; }
        public string O { get; set; }
        public int A { get; set; }
        public int V { get; set; }
        public int? ID { get; set; }
    }

    public class UpitnikProsjek
    {
        public int G { get; set; }
        public string O { get; set; }
        public int V { get; set; }
    }

    public class UpitnikRezultatItem
    {
        public int Grupa { get; set; }
        public int Vrijednost { get; set; }
    }

    public class UpitnikRezultat
    {
        public UpitnikRezultatItem[] Grupe { get; set; }
        public int Ocjena { get; set; }
    }

    public class Upitnik
    {
        public int? UpitnikId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime Datum { get; set; }

        public Upitnik()
        {
            Datum = DateTime.Now;
        }

        public IList<UpitnikVrijednostItem> Vrijednosti { get; set; }
        public IList<UpitnikProsjek> Prosjek { get; set; }
    }

    public class UpitnikVrijednost
    {
        public int? Id { get; set; }
        public int GrupaId { get; set; }
        public string Oznaka { get; set; }
        public string Opis { get; set; }
        public decimal Faktor { get; set; }
        public bool Aktivno { get; set; }
    }

    public class UpitnikVrijednostRezultat : UpitnikVrijednost
    {
        public int PitanjeId { get; set; }
        public UpitnikVrijednostRezultat() {
            Vrijednosti = new int?[10];
        }
        public int?[] Vrijednosti { get; set; }
    }

    public class UpitnikGraf
    {
        public int UpitnikId { get; set; }
        public DateTime Datum { get; set; }
        public string Naziv { get; set; }
        public int[] BP { get; set; }
        public int[] AV { get; set; }
        public int[] QR { get; set; }

        public UpitnikGraf()
        {
            BP = new int[9];
            AV = new int[9];
            QR = new int[9];
        }
    }
}