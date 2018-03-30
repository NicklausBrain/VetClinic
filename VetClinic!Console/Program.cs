using System;
using VetClinic.Domain;
using VetClinic.Persistence.Dapper;
using VetClinic.Persistence.SiaqoDb;

namespace VetClinic.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			TestDapper();
			//TestSiaqodb();
		}

		private static void TestDapper()
		{
			var connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=VetClinic;Integrated Security=True";

			var ownerRepository = new DapperOwnerRepository(connectionString);
			var petRepository = new DapperPetRepository(connectionString, ownerRepository);
			var medicalRecordRepository = new DapperMedicalRecordRepository(connectionString, petRepository);

			var petRegistry = new PetRegistry(
				new DapperUnitOfWork(
					medicalRecordRepository,
					ownerRepository,
					petRepository));

			petRegistry.NewRecord(new Pet(
				id: Guid.NewGuid(),
				name: "Oskar",
				birthday: DateTimeOffset.Now,
				sex: Sex.Female, kind: PetKind.Dog,
				description: "crazy dog",
				image: "https://130d8a.dm.files.1drv.com/y4mXEOG4tqerwHf",
				getOwner: () => new Owner(
					id: Guid.NewGuid(),
					name: "Marina",
					surname: "Brain",
					address: new Address(
						city: "Chernigiv",
						street: "Radchenko",
						building: "1",
						addressLine: "2"))));

		}

		private static void TestSiaqodb()
		{
			Guid ownerId1, ownerId2;

			var ownerRepository = new SqoOwnerRepository(@"c:\Siaqodb\");

			ownerId1 = ownerRepository.Save(new Owner(
				id: Guid.NewGuid(),
				name: "Anastasia",
				surname: "Brain",
				address: new Address(
					city: "Chernigiv",
					street: "Radchenko",
					building: "1",
					addressLine: "2")));

			ownerId2 = ownerRepository.Save(
				new Owner(
					id: Guid.NewGuid(),
					name: "Nick",
					surname: "Brain",
					address: new Address(
						city: "Chernigiv",
						street: "Radchenko",
						building: "1",
						addressLine: "2")));

			foreach (var owner in ownerRepository.GetAll())
			{
				Console.WriteLine(owner.Name + " from " + owner.Address.City);
			}

			ownerRepository.Remove(ownerId1);
			ownerRepository.Remove(ownerId2);
		}
	}
}
