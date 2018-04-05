using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using VetClinic.Domain;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.Dapper
{
	public class DapperPetRepository : IPetRepository
	{
		private readonly string connectionString;
		private readonly IOwnerRepository owners;

		public DapperPetRepository(
			string connectionString, IOwnerRepository owners)
		{
			this.connectionString = connectionString;
			this.owners = owners;
		}

		public IEnumerable<Pet> GetAll()
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				// Query is a Dapper method
				var rawPets = sql.Query<dynamic>("SELECT * FROM Pets"); // sql script

				var pets = rawPets.Select(pet => new Pet(
					id: pet.Id,
					name: pet.Name,
					birthday: pet.Birthday,
					sex: pet.Sex,
					kind: pet.Kind,
					description: pet.Description,
					image: pet.Image,
					getOwner: new Func<Owner>(() => this.owners.GetById(pet.OwnerId)))); // Note! There is no OwnerId in Pet object!

				return pets;
			}
		}

		public Pet GetById(Guid id)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				var pet = sql.QueryFirst("SELECT * FROM Pets WHERE Id = @Id", new { Id = id });

				return new Pet(
					id: pet.Id,
					name: pet.Name,
					birthday: pet.Birthday,
					sex: (Sex)pet.Sex,
					kind: (PetKind)pet.Kind,
					description: pet.Description,
					image: pet.Image,
					getOwner: new Func<Owner>(() => this.owners.GetById(pet.OwnerId)));
			}
		}

		public Guid Save(Pet pet)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				sql.Execute(
					"INSERT INTO Pets VALUES (@Id, @Name, @Birthday, @Sex, @Kind, @Description, @Image, @OwnerId)",
					new
					{
						pet.Id,
						pet.Name,
						pet.Birthday,
						pet.Sex,
						pet.Kind,
						pet.Description,
						pet.Image,
						OwnerId = pet.Owner.Id
					});
			}

			return pet.Id;
		}

		public void Remove(Guid id)
		{
			using (var connection = new SqlConnection(this.connectionString))
			{
				connection.Execute("DELETE FROM Pets WHERE Id = @Id", id);
			}
		}
	}
}
