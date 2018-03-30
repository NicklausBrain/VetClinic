using System;

namespace VetClinic.Domain.Contracts
{
	public interface IUnitOfWork
	{
		IMedicalRecordRepository MedicalRecors { get; }

		IOwnerRepository Owners { get; }

		IPetRepository Pets { get; }

		ILogicalTransaction Begin();
	}

	public interface ILogicalTransaction : IDisposable
	{
		void Done(); // End // Commit // Finish // Save
	}
}
