using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dapper;
using VetClinic.Domain;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.Dapper
{
	public class DapperMedicalRecordRepository : IMedicalRecordRepository
	{
		private readonly string connectionString;
		private readonly DapperPetRepository petRepository;

		public DapperMedicalRecordRepository(
			string connectionString,
			DapperPetRepository petRepository)
		{
			this.connectionString = connectionString;
			this.petRepository = petRepository;
		}

		public IEnumerable<MedicalRecord> GetAll()
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				var recordsIds = sql.Query<Guid>("SELECT Id FROM MedicalRecords;");

				foreach (var recordId in recordsIds)
				{
					yield return this.GetById(recordId);
				}
			}
		}

		public MedicalRecord GetById(Guid recordId)
		{
			using (var sql = new SqlConnection(this.connectionString))
			{
				using (var recordData = sql.QueryMultiple(
					@"SELECT * FROM MedicalRecords WHERE Id = @Id;
						  SELECT * FROM Treatments WHERE MedicalRecordId = @Id;",
					new { Id = recordId }))
				{
					var record = recordData.Read<dynamic>().First();
					var medicalHistory = recordData.Read<Treatment>();

					return new MedicalRecord(record.Id, this.petRepository.GetById(record.PetId), medicalHistory);
				}
			}
		}

		public Guid Save(MedicalRecord record)
		{
			using (var transaction = new TransactionScope())
			using (var sql = new SqlConnection(this.connectionString))
			{
				sql.Execute(
					"INSERT INTO MedicalRecords VALUES (@Id, @PetID);",
					new { Id = record.Id, PetId = record.Pet.Id });

				foreach (var treatment in record.MedicalHistory)
				{
					sql.Execute(
						"INSERT INTO Treatments VALUES (@Id, @Date, @Description, @Price);",
						new
						{
							Id = treatment.Id,
							Date = treatment.Date,
							Description = treatment.Description,
							Price = treatment.Price,
							MedicalRecordId =record.Id
						});
				}

				transaction.Complete();
			}

			return record.Id;
		}

		public void Remove(Guid id)
		{
			throw new NotImplementedException();
		}

		public void RecordTreatment(Treatment treatment)
		{
			throw new NotImplementedException();
		}
	}
}
