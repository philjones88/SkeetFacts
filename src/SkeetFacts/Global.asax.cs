using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.MvcIntegration;
using SkeetFacts.Models;

namespace SkeetFacts
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static IDocumentStore DocumentStore { get; private set; }

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
            // Only use the Razor view engine, this saves wasted lookups when resolving view names
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            InitializeDocumentStore();
        }

        private static void InitializeDocumentStore()
        {
            if (DocumentStore != null) return; // prevent misuse

            DocumentStore = new DocumentStore
            {
                ConnectionStringName = "RAVENHQ_CONNECTION_STRING"
            }.Initialize();

            TryCreatingIndexesOrRedirectToErrorPage();

            RavenProfiler.InitializeFor(DocumentStore, "Email", "Password", "Username");
            //Fields to filter out of the output
        }

        private static void TryCreatingIndexesOrRedirectToErrorPage()
        {
            try
            {
                IndexCreation.CreateIndexesAsync(Assembly.GetAssembly(typeof (Fact)), DocumentStore);
            }
            catch (WebException e)
            {
                var socketException = e.InnerException as SocketException;
                if (socketException == null)
                    throw;

                switch (socketException.SocketErrorCode)
                {
                    case SocketError.AddressNotAvailable:
                    case SocketError.NetworkDown:
                    case SocketError.NetworkUnreachable:
                    case SocketError.ConnectionAborted:
                    case SocketError.ConnectionReset:
                    case SocketError.TimedOut:
                    case SocketError.ConnectionRefused:
                    case SocketError.HostDown:
                    case SocketError.HostUnreachable:
                    case SocketError.HostNotFound:
                        HttpContext.Current.Response.Redirect("~/offline.html");
                        break;
                    default:
                        throw;
                }
            }
        }
    }
}