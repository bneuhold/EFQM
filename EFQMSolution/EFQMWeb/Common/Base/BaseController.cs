using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFQMWeb.Common.DB;
using Microsoft.Practices.Unity;

namespace EFQMWeb.Common.Base
{
    public class BaseController : Controller
    {
        private readonly static string[] _jsonTypes = new string[] { "application/json", "text/json" };
        private readonly static string[] _htmlType = new string[] { "text/html" };
        private readonly static string[] _xmlTypes = new string[] { "application/xml", "text/xml" };

        public static bool IsJsonTrafic(HttpContext httpContex)
        {
            var acceptTypes = httpContex.Request.AcceptTypes;
            return (_jsonTypes.Any(type => acceptTypes.Contains(type)));
        }

        public static bool IsAjaxRequest(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return (request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
        }

        public DBOperations Database
        {
            get
            {
                return getContainer().Resolve<DBOperations>();
            }
        }

        public string ClientType { get; set; }

        public IUnityContainer getContainer()
        {
            if (HttpContext == null)
                return null;

            IContainerAccessor accessor = HttpContext.ApplicationInstance as IContainerAccessor;
            if (accessor == null)
                return null;
            return accessor.Container;
        }
    }
}