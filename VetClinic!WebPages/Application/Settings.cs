using System.Web.Configuration;
using System.Web.Hosting;

namespace VetClinic.WebPages.Application
{
	public static class Settings
	{
		public static string DataPath => HostingEnvironment.MapPath("~/data");

		public static string PetImagesPath => HostingEnvironment.MapPath("~/data/petImages");

		public static string PetRepositoryPath => HostingEnvironment.MapPath("~/data/petRepository.xml");

		public static string ConnectionString => WebConfigurationManager.AppSettings["connectionString"];
	}
}