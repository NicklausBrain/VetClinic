using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VetClinic.Mvc.Attributes
{
	public class LogActionAttribute: ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var parameters = filterContext.ActionParameters;

			Console.WriteLine(parameters);
		}
	}
}