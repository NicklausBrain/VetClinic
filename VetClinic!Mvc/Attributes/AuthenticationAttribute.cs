using System.Web.Mvc.Filters;

namespace VetClinic.Mvc.Attributes
{
	public class AuthenticationAttribute: IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			//Here you are setting current principal
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			//Here you're checking current action and redirecting to ErrorPage
		}
	}
}