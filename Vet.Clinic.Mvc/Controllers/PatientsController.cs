using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VetClinic.Domain;
using VetClinic.Mvc.Application;
using VetClinic.Mvc.Models;
using VetClinic.Persistence.Dapper;

namespace VetClinic.Mvc.Controllers
{
	public class PatientsController : Controller
	{
		private readonly PetRegistry petRegistry;

		public PatientsController() //PetRegistry petRegistry
		{
			// this.petRegistry = petRegistry;

			// TODO: move dependencies initialization to container
			var connectionString = Settings.ConnectionString;
			var ownerRepository = new DapperOwnerRepository(connectionString);
			var petRepository = new DapperPetRepository(connectionString, ownerRepository);
			var medicalRecordRepository = new DapperMedicalRecordRepository(connectionString, petRepository);

			this.petRegistry = new PetRegistry(
				new DapperUnitOfWork(
					medicalRecordRepository,
					ownerRepository,
					petRepository));
		}

		[HttpGet]
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
		public ActionResult Create(NewPatientModel patient)
		{
			if (ModelState.IsValid)
			{
				var imagePath = $@"{Settings.PetImagesPath}\{patient.Image.FileName}";

				// TODO: this is nasty
				using (var file = System.IO.File.Create(imagePath))
				{
					patient.Image.InputStream.CopyTo(file);
				}

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