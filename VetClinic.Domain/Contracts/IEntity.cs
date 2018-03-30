using System;

namespace VetClinic.Domain.Contracts
{
	public interface IEntity
	{
		Guid Id { get; }
	}
}