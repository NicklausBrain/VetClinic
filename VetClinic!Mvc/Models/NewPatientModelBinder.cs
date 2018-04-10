using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VetClinic.Domain;
using VetClinic.Mvc.Application;

namespace VetClinic.Mvc.Models
{
	public class NewPatientModelBinder: DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			HttpRequestBase request = controllerContext.HttpContext.Request;

			var image = request.Files[nameof(NewPatientModel.Image)];

			var imagePath = $@"{Settings.PetImagesPath}\{image.FileName}";

			using (var file = System.IO.File.Create(imagePath))
			{
				image.InputStream.CopyTo(file);
			}

			var model = new NewPatientModel
			{
				Name = request.Form[nameof(NewPatientModel.Name)],
				Birthday = DateTime.Parse(request.Form[nameof(NewPatientModel.Birthday)]),
				Description = request.Form[nameof(NewPatientModel.Description)],
				Kind = (PetKind)Enum.Parse(typeof(PetKind), request.Form[nameof(NewPatientModel.Kind)]),
				Sex = (Sex)Enum.Parse(typeof(Sex), request.Form[nameof(NewPatientModel.Sex)]),
				Image = image
			};

			return model;
		}
	}
}