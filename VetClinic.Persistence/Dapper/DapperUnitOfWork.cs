using System.Transactions;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.Dapper
{
	public class DapperUnitOfWork : IUnitOfWork
	{
		public DapperUnitOfWork(
			DapperMedicalRecordRepository medicalRecors,
			DapperOwnerRepository owners,
			DapperPetRepository pets)
		{
			this.MedicalRecors = medicalRecors;
			this.Owners = owners;
			this.Pets = pets;
		}

		public IMedicalRecordRepository MedicalRecors { get; }

		public IOwnerRepository Owners { get; }

		public IPetRepository Pets { get; }

		public ILogicalTransaction Begin()
		{
			return new SqlTransaction();
		}

		private class SqlTransaction : ILogicalTransaction
		{
			private readonly TransactionScope ts;

			public SqlTransaction()
			{
				ts = new TransactionScope();
			}

			public void Done()
			{
				ts.Complete();
			}

			public void Dispose()
			{
				ts?.Dispose();
			}
		}
	}
}
