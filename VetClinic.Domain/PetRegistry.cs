using System;
using System.Collections.Generic;
using System.Linq;
using VetClinic.Domain.Contracts;

namespace VetClinic.Domain
{
	public class PetRegistry
	{
		private readonly IUnitOfWork unitOfWork;

		public PetRegistry(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public MedicalRecord NewRecord(Pet pet)
		{
			var newRecord = new MedicalRecord(
				id: Guid.NewGuid(),
				getPet: () => pet,
				medicalHistory: Enumerable.Empty<Treatment>());

			using (var work = this.unitOfWork.Begin())
			{
				this.unitOfWork.Owners.Save(pet.Owner);

				this.unitOfWork.Pets.Save(pet);

				this.unitOfWork.MedicalRecors.Save(newRecord);

				work.Done();
			}

			return newRecord;
		}

		public void Remove(Guid recordId)
		{
			var record = this.unitOfWork.MedicalRecors.GetById(recordId);

			using (var work = this.unitOfWork.Begin())
			{
				this.unitOfWork.MedicalRecors.Remove(recordId);

				this.unitOfWork.Pets.Remove(record.Pet.Id);

				work.Done();
			}
		}

		public IEnumerable<MedicalRecord> Find(Func<MedicalRecord, bool> filter = null)
		{
			return filter == null
				? this.unitOfWork.MedicalRecors.GetAll()
				: this.unitOfWork.MedicalRecors.GetAll().Where(filter);
		}
	}
}
