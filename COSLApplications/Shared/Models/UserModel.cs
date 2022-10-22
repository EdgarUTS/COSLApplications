using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using COSLApplications.Shared.Validations;

namespace COSLApplications.Shared.Models
{
	[Table("UserModel", Schema = "dbo")]
	[Index(nameof(Email), IsUnique = true)]
	[Index(nameof(UserID), IsUnique = true)]
	public class UserModel : IValidatableObject
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		public bool IsActive { get; set; } = true;
		public int UserID { get; set; }
		[Required(ErrorMessage = "First name, it is need")]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		//[SupplierValidation(ErrorMessage = "Wrong value entered", ValidSupplierValue = "Code-Maze")]
		public string LastName { get; set; }
		[Display(Name = "Email")]
		[EmailValidator()]
		public string Email { get; set; }
		[Required]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		public string Company { get; set; }
		public string? Address { get; set; } = "";
		public string? City { get; set; } = "";
		public string? Region { get; set; } = "";
		public string? Department { get; set; } = "";
		[Display(Name = "Create Time")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public string? Position { get; set; } = "";
		[Display(Name = "Group")]
		public int? ItGroup { get; set; } = 0;
		[Display(Name = "Office Location")]
		public string? OfficePosition { get; set; } = "";
		public List<Training> IncidentsDetails { get; set; } = new List<Training>();
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (LastName == FirstName)
			{
				yield return new ValidationResult(
					"Blog Title cannot match Blogger Name",
					new[] { nameof(FirstName), nameof(LastName) });
			}

		}
	}
}
