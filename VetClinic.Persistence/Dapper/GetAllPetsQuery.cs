using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using VetClinic.Domain;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.Dapper
{
	public class GetAllPetsQuery: IGetAllPetsQuery
	{
		private string connectionString;

		public GetAllPetsQuery(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IEnumerable<Pet> Execute()
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
	}
}
