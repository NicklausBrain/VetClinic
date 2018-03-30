using System;
using System.Collections.Generic;
using Sqo;
using VetClinic.Domain;
using VetClinic.Domain.Contracts;

namespace VetClinic.Persistence.SiaqoDb
{
	public class SqoOwnerRepository : IOwnerRepository
	{
		private readonly Siaqodb siaqodb;

		public SqoOwnerRepository(Siaqodb databasePath)
		{
			this.siaqodb = databasePath;
		}

		public SqoOwnerRepository(string databasePath)
		{
			this.siaqodb = new Siaqodb(databasePath);
		}

		public IEnumerable<Owner> GetAll()
		{
			return this.siaqodb.LoadAll<Owner>();
		}

		public Owner GetById(Guid id)
		{
			return this.siaqodb.Query<Owner>().First(owner => owner.Id == id);
		}

		public Guid Save(Owner owner)
		{
			this.siaqodb.StoreObject(owner);

			return owner.Id;
		}

		public void Remove(Guid id)
		{
			this.siaqodb.DeleteObjectBy(nameof(Owner.Id), id);
		}
	}
}
