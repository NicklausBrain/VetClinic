using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using VetClinic.Domain;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.Dapper
{
	public class DapperOwnerRepository : IOwnerRepository
	{
		private readonly string connectionString;

		public DapperOwnerRepository(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IEnumerable<Owner> GetAll()
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				return sql.Query<Owner>("SELECT * FROM Owners"); // using built-in mapping
			}
		}

		public Owner GetById(Guid id)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				return sql
					.Query<dynamic>("SELECT * FROM Owners", id)
					.Select(owner => new Owner(
						id: owner.Id,
						name: owner.Name,
						surname: owner.Surname,
						address: new Address(
							owner.City,
							owner.Street,
							owner.Building,
							owner.AddressLine)))
					.Single();
			}
		}

		public Guid Save(Owner owner)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				sql.Execute("INSERT INTO Owners VALUES (@Id, @Name, @Surname, @City, @Street, @Building, @AddressLine)", new
				{
					owner.Id,
					owner.Name,
					owner.Surname,
					owner.Address.City,
					owner.Address.Street,
					owner.Address.Building,
					owner.Address.AddressLine
				});
			}

			return owner.Id;
		}

		public void Remove(Guid id)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				sql.Execute("DELETE FROM Owners WHERE Id = @Id", new { Id = id });
			}
		}
	}
}
