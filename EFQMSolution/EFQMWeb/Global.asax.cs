using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EFQMWeb.Common.DB;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using System.Web.Security;

namespace EFQMWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IContainerAccessor
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            CreateContainer();

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            DBOperations.Database = Container.Resolve<DBOperations>();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {


            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                /*if (ticket.Expired)
                {
                    FormsAuthentication.RenewTicketIfOld(ticket);
                }*/
                FormsAuthentication.RenewTicketIfOld(ticket);
            }



        }

        private static IUnityContainer _container = null;

        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
            private set
            {
                _container = value;
            }
        }

        IUnityContainer IContainerAccessor.Container
        {
            get
            {
                return Container;
            }
        }

        protected virtual void CreateContainer()
        {
            _container = new UnityContainer();
            _container.AddNewExtension<EnterpriseLibraryCoreExtension>();
            //ReaderDb je singleton objekt
            _container.RegisterType<DBOperations>(new ContainerControlledLifetimeManager());


        }
    }


    public interface IContainerAccessor
    {
        IUnityContainer Container
        {
            get;
        }
    }

}