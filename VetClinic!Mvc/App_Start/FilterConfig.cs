using System.Web.Mvc;
using VetClinic.Mvc.Attributes;

namespace VetClinic.Mvc
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());

			//filters.Add(new AuthenticationAttribute());
		}
	}
}
