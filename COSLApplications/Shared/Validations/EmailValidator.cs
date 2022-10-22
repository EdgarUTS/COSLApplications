using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;

//using Microsoft.AspNetCore.Components;


namespace COSLApplications.Shared.Validations
{
	public class EmailValidator : ValidationAttribute
	{
		//[Parameter]
		//public object dbc { get; set; }
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			//HttpClient Http = new();
			//ValidCheck vc = new();
			//vc.command = "unique";
			//vc.dataType = "string";
			//vc.dataValue = "q";
			//vc.result = "";
			//vc.fieldName = "Email";
			//Task<HttpResponseMessage>? response=null;
			//try
			//{//how to set the absoult uri?
			//	response = Http.PostAsJsonAsync<ValidCheck>("api/User/check", vc);
			//}
			//catch (Exception e) { 
			//	Console.WriteLine(e.Message);
			//	Http.Dispose(); }

			//var res = response.Result.Content.ReadFromJsonAsync<ValidCheck>();



			//return new ValidationResult("Email needs @", new[] { validationContext.MemberName });
			return null;
		}
	}
}
