using System;
using System.Collections.Generic;

namespace VetClinic.Domain.Contracts
{
	public interface IRepository<T> where T : IEntity
	{
		IEnumerable<T> GetAll();

		T GetById(Guid id);

		Guid Save(T entity);

		void Remove(Guid id);
	}
}
