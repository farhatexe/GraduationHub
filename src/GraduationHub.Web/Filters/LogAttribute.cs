using System.Collections.Generic;
using System.Web.Mvc;
using GraduationHub.Web.Data;
using GraduationHub.Web.Domain;
using GraduationHub.Web.Infrastructure;

namespace GraduationHub.Web.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;

        public LogAttribute(string description)
        {
            Description = description;
        }

        public ApplicationDbContext Context { get; set; }
        public ICurrentUser CurrentUser { get; set; }

        public string Description { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string description = Description;

            foreach (var kvp in _parameters)
            {
                description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
            }

            Context.Logs.Add(new LogAction(CurrentUser.User, filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, description));

            Context.SaveChanges();
        }
    }
}