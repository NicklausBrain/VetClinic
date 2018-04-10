using System;
using System.Web.Mvc;

namespace VetClinic.Mvc.Attributes
{
	public class AuthorizationAttribute: AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			throw new NotImplementedException();
		}
	}
}