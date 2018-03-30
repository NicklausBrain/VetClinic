using System;
using System.Collections.Generic;
using VetClinic.Domain.Contracts;

namespace VetClinic.Domain
{
	public class MedicalRecord : IEntity
	{
		private readonly Lazy<Pet> pet;

		public MedicalRecord(
			Guid id,
			Func<Pet> getPet,
			IEnumerable<Treatment> medicalHistory)
		{
			this.Id = id;
			this.pet = new Lazy<Pet>(getPet);
			this.MedicalHistory = medicalHistory;
		}

		public Guid Id { get; }

		public Pet Pet => pet.Value;

		public IEnumerable<Treatment> MedicalHistory { get; }
	}
}
