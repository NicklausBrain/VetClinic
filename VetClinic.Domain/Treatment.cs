using System;
using VetClinic.Domain.Contracts;

namespace VetClinic.Domain
{
	public class Treatment : IEntity//??
	{
		public Treatment(
			Guid id,
			DateTimeOffset date,
			string description,
			decimal price)
		{
			this.Id = id;
			this.Date = date;
			this.Description = description;
			this.Price = price;
		}

		public Guid Id { get; }

		public DateTimeOffset Date { get; }

		public string Description { get; }

		public Decimal Price { get; }
	}
}
