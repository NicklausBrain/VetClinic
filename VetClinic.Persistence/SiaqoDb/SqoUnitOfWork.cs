using Sqo;
using Sqo.Transactions;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.SiaqoDb
{
	public class SqoUnitOfWork:IUnitOfWork
	{
		private readonly Siaqodb siaqodb;

		public SqoUnitOfWork(string databasePath)
		{
			this.siaqodb = new Siaqodb(databasePath);

			this.Owners = new SqoOwnerRepository(this.siaqodb);
		}

		public IMedicalRecordRepository MedicalRecors { get; }

		public IOwnerRepository Owners { get; }

		public IPetRepository Pets { get; }

		public ILogicalTransaction Begin()
		{
			return new SqoTransaction(siaqodb.BeginTransaction());
		}

		private class SqoTransaction: ILogicalTransaction
		{
			private readonly ITransaction sqoTransaction;

			public SqoTransaction(ITransaction sqoTransaction)
			{
				this.sqoTransaction = sqoTransaction;
			}

			public void Dispose()
			{
				sqoTransaction.Dispose();
			}

			public void Done()
			{
				this.sqoTransaction.Commit();
			}
		}
	}
}
