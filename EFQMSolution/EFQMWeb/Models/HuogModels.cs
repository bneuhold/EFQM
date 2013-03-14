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

    }

    public class UpitnikVrijednostItem
    {
        public int P { get; set; }
        public string O { get; set; }
        public int A { get; set; }
        public int V { get; set; }
    }

    public class Upitnik
    {
        public int? UpitnikId { get; set; }

        public IList<UpitnikVrijednostItem> Vrijednosti { get; set; }
    }
}