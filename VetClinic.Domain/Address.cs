namespace VetClinic.Domain
{
	public struct Address
	{
		public Address(
			string city,
			string street,
			string building,
			string addressLine)
		{
			this.City = city;
			this.Street = street;
			this.Building = building;
			this.AddressLine = addressLine;
		}

		public string City { get; }

		public string Street { get; }

		public string Building { get; }

		public string AddressLine { get; }
	}
}
