using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using COSLApplications.Shared.Validations;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace COSLApplications.Shared.Models
{
	[Table("Training", Schema = "dbo")]
	
	public class Training
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		[Display(Name = "Certification Number")]
		public string? CertNo { get; set; }
		[Required]
		public string? Result { get; set; }
		[Required]
		[Display(Name = "Issue Date")]
		public DateOnly IssueDate { get; set; }
		[Required]
		[Display(Name = "Expiry Date")]
		public DateOnly Expir { get; set; }
		[Required]
		[Display(Name = "Training Center")]
		public string? TrainingCenter { get; set; }
		//public Guid UserModelId { get; set; }
		[Display(Name = "Days left")]
		public int LeftDays
		{
			get
			{
				return (Expir.DayNumber-IssueDate.DayNumber);
			}
		}
	}
}