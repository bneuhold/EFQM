using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EFQMWeb.Common.Entity;

namespace EFQMWeb.Common.Util
{
    public class MySession
    {

        public static LoggedUser CurrentUser{
            get
            {
                return (LoggedUser)HttpContext.Current.Session["CurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"]= value;
            }
        }
    }
}