using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using VetClinic.Domain;

namespace VetClinic.Mvc.Models
{
	public class NewPatientModel
	{
		[Required]
		[MinLength(2)]
		[MaxLength(50)]
		public string Name { get; set; }

		public DateTime Birthday { get; set; }

		public Sex Sex { get; set; }

		public PetKind Kind { get; set; }

		[Required]
		[MinLength(10)]
		[MaxLength(500)]
		public string Description { get; set; }

		[Required]
		public HttpPostedFileBase Image { get; set; }
	}
}