using System;
using VetClinic.Domain.Contracts;

namespace VetClinic.Domain
{
	public class Owner : IEntity
	{
		public Owner(
			Guid id,
			string name,
			string surname,
			Address address)
		{
			this.Id = id;
			this.Name = name;
			this.Surname = surname;
			this.Address = address;
		}

		public Owner(
			Guid id,
			string name,
			string surname,
			string city,
			string street,
			string building,
			string addressLine) :
				this(
					id: id,
					name: name,
					surname: surname,
					address: new Address(
						city: city,
						street: street,
						building: building,
						addressLine: addressLine))
		{
		}

		public Guid Id { get; }

		public string Name { get; }

		public string Surname { get; }

		public Address Address { get; }
	}
}