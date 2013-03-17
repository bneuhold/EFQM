using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace EFQMWeb.Common.Util
{
    public class MyConfig
    {

        public static string GetSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        
    }
}