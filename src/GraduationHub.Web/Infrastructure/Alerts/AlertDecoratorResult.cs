using System.Web.Mvc;

namespace GraduationHub.Web.Infrastructure.Alerts
{
	public class AlertDecoratorResult : ActionResult
	{
		public ActionResult InnerResult { get; set; }
		public string AlertClass { get; set; }
		public string Message { get; set; }

		public AlertDecoratorResult(ActionResult innerResult, string alertClass, string message)
		{
			InnerResult = innerResult;
			AlertClass = alertClass;
			Message = message;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var alerts = AlertExtensions.GetAlerts(context.Controller.TempData);
			alerts.Add(new Alert(AlertClass, Message));
			InnerResult.ExecuteResult(context);
		}
	}
}