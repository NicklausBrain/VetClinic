using System;
using VetClinic.Domain.Contracts;

namespace VetClinic.Domain
{
	public class Pet : IEntity
	{
		private readonly Lazy<Owner> owner;

		public Pet(
			Guid id,
			string name,
			DateTimeOffset birthday,
			Sex sex,
			PetKind kind,
			string description,
			string image,
			Func<Owner> getOwner)
		{
			this.Id = id;
			this.Name = name;
			this.Birthday = birthday;
			this.Sex = sex;
			this.Kind = kind;
			this.Description = description;
			this.Image = image;
			this.owner = new Lazy<Owner>(getOwner);
		}

		public Guid Id { get; }

		public string Name { get; }

		public DateTimeOffset Birthday { get; }

		public Sex Sex { get; }

		public PetKind Kind { get; }

		public string Description { get; }

		public string Image { get; }

		public Owner Owner => this.owner.Value;
	}
}