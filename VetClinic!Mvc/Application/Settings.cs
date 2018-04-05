using System.Web.Configuration;
using System.Web.Hosting;

namespace VetClinic.Mvc.Application
{
	public class Settings
	{
		public static string DataPath => HostingEnvironment.MapPath("~/data");

		public static string PetImagesPath => HostingEnvironment.MapPath("~/data/petImages");

		public static string PetRepositoryPath => HostingEnvironment.MapPath("~/data/petRepository.xml");

		public static string ConnectionString => WebConfigurationManager.AppSettings["connectionString"];
	}
}