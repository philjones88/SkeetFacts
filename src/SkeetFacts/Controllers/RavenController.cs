using System.Web.Mvc;
using Raven.Client;

namespace SkeetFacts.Controllers
{
    public class RavenController : Controller
    {
        public static IDocumentStore DocumentStore { get; set; }

        private IDocumentSession _ravenSession;

        public IDocumentSession RavenSession
        {
            get { return _ravenSession ?? (_ravenSession = DocumentStore.OpenSession()); }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (_ravenSession)
            {
                if (_ravenSession != null && filterContext.Exception == null)
                    _ravenSession.SaveChanges();
            }
        }

    }
}
