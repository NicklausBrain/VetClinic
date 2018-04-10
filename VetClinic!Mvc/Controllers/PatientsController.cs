using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Mvc;
using VetClinic.Domain;
using VetClinic.Mvc.Application;
using VetClinic.Mvc.Attributes;
using VetClinic.Mvc.Models;
using VetClinic.Persistence.Dapper;

namespace VetClinic.Mvc.Controllers
{
	[Authorize()]
	public class PatientsController : Controller
	{
		private readonly PetRegistry petRegistry;

		public PatientsController(PetRegistry petRegistry)
		{
			//var user = this.User;
			//var claims = ((ClaimsIdentity) user.Identity).Claims;

			//var roles = claims.Where(c => c.Type == ((ClaimsIdentity) user.Identity).RoleClaimType);

			this.petRegistry = petRegistry;
		}

		[HttpGet]
		[LogAction]
		public ActionResult Index(string searchString)
		{
			IEnumerable<Pet> pets;

			if (string.IsNullOrEmpty(searchString))
			{
				pets = this.petRegistry.Find().Select(card => card.Pet);
			}
			else
			{
				pets = this.petRegistry
					.Find().Select(card => card.Pet)
					.Where(pet =>
						pet.GetType().GetProperties().Select(p =>
							p.GetValue(pet).ToString().ToLower()).Any(s => s.Contains(searchString.ToLower())));
			}

			return this.View("Patients", pets);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return this.View("NewPatient");
		}

		[HttpPost]
		public ActionResult Create([ModelBinder(typeof(NewPatientModelBinder))] NewPatientModel patient)
		{
			if (ModelState.IsValid)
			{
				var record = this.petRegistry.NewRecord(new Pet(
					id: Guid.NewGuid(),
					name: patient.Name,
					birthday: patient.Birthday,
					sex: patient.Sex,
					kind: patient.Kind,
					description: patient.Description,
					image: patient.Image.FileName,
					getOwner: () => new Owner( // todo: hardcoded owner
						id: Guid.NewGuid(),
						name: "Marina",
						surname: "Brain",
						address: new Address(
							city: "Chernigiv",
							street: "Radchenko",
							building: "1",
							addressLine: "2"))));

				return this.View("Success");
			}

			return this.View("NewPatient", patient);
		}
	}
}