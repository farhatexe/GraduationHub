using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GraduationHub.Web.Controllers;
using GraduationHub.Web.Infrastructure.Tasks;

namespace GraduationHub.Web.Infrastructure
{
    public class NotFoundError : IRunOnError
    {
        private readonly HttpContextBase _context;
        private readonly HttpResponseBase _response;

        public NotFoundError(HttpContextBase context, HttpResponseBase response)
        {
            _context = context;
            _response = response;
        }

        public void Execute()
        {
            if (_context.Response.StatusCode == 404)
            {
                _response.Clear();

                var rd = new RouteData();
                rd.Values["controller"] = "Errors";
                rd.Values["action"] = "NotFound";

                IController c = new ErrorsController();
                c.Execute(new RequestContext(_context, rd));
            }
        }
    }
}