using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;

namespace COSLApplications.Shared.Models
{

	[Table("HSEIncidentsModel", Schema = "dbo")]
	[Index(nameof(IncidentID), nameof(IncidentVersion), IsUnique = true)]
	public class HSEIncidentsModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		[NotNull]
		[StringLength(20)]
		public string IncidentID { get; set; }
		[Required]
		[NotNull]
		public int IncidentVersion { get; set; } = 0;
		public DateTime CreateTime { get; set; } = DateTime.UtcNow;
		[Required, NotNull]
		[StringLength(100)]
		[Display(Name = "Summary")]
		public string Abstract { get; set; } = "n/a";
		//public string CreatedBy { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MMM-dd HH:mm}", ApplyFormatInEditMode = true)]

		public DateTime LastUpdateTime { get; set; } = DateTime.Now;
		[StringLength(30)]
		public string? LastUpdateBy { get; set; } = "n/a";
		[StringLength(30)]
		public string? ReportPerson { get; set; } = "n/a";
		public DateTime IncidentTime { get; set; } = DateTime.Now;
		public DateTime ReportTime { get; set; } = DateTime.Now;
		[Required]
		public IncidentLevel FinalLevel { get; set; } = 0;
		[Required]
		public IncidentClass FinalIncidentClass { get; set; } = 0;
		[Required]
		public CaseStatus CurrentStatus { get; set; } = 0;
		[Required, NotNull]
		[StringLength(30)]
		public string? Customer { get; set; } = "n/a";
		[StringLength(30)]
		public string? CustomerRep { get; set; } = "n/a";
		[StringLength(50)]
		public string? CustomerRepContact { get; set; } = "n/a";
		[Required, NotNull]
		[StringLength(30)]
		public string? Contractor { get; set; } = "n/a";
		[StringLength(30)]
		public string? ContractorManager { get; set; } = "n/a";
		[StringLength(50)]
		public string? ContractorManagerContact { get; set; } = "n/a";
		[StringLength(30)]
		public string? Country { get; set; } = "n/a";
		public string? Field { get; set; } = "n/a";
		[Required]
		[StringLength(50)]
		public string? OpsSite { get; set; } = "n/a";
		[StringLength(30)]
		public string? FieldManagers { get; set; } = "n/a";
		[StringLength(50)]
		public string? FieldManagerContact { get; set; } = "n/a";
		[StringLength(30)]
		public string? FieldHSE { get; set; } = "n/a";
		[StringLength(50)]
		public string? FieldHSEContact { get; set; } = "n/a";
		public int NPTLost { get; set; } = 0;
		public int NPTCost { get; set; } = 0;
		[StringLength(30)]
		public string WeatherCondition { set; get; } = "n/a";
		[StringLength(50)]
		public string SiteCondition { set; get; } = "n/a";

		public List<IncidentsDetails> IncidentsDetails { get; set; } = new List<IncidentsDetails>();
		public List<EvidenceDoc> EvidenceDoc { get; set; } = new List<EvidenceDoc>();

		public HSEIncidentsModel ShallowCopy()
		{
			return (HSEIncidentsModel)this.MemberwiseClone();
		}
		public HSEIncidentsModel DeepCopy()
		{
			HSEIncidentsModel other = (HSEIncidentsModel)this.MemberwiseClone();
			//other.FinalLevel = new Course(Course_ID.Course_Id);
			//other.Name = String.Copy(Name);
			return other;
		}
	}

		public enum IncidentLevel
	{
		[Display(Name = "Level 0")] level_0 = 1,
		[Display(Name = "Level 1")] level_1 = 2,
		[Display(Name = "Level 2")] level_2 = 3,
		[Display(Name = "Level 3")] level_3 = 4,
		[Display(Name = "Others")] Others = 0
	}
	public enum CaseStatus
	{
		[Display(Name = "Initial")] initial = 1,
		[Display(Name = "Open")] open = 2,
		[Display(Name = "Closed")] closed = 3,
		[Display(Name = "Review")] review = 4,
		[Display(Name = "Re-Open")] reopen = 5,
		[Display(Name = "Undefined")] NONE = 0
	}
	public enum IncidentClass
	{
		[Display(Name = "Class A")] classA = 1,
		[Display(Name = "Class B")] classB = 2,
		[Display(Name = "Class C")] classC = 3,
		[Display(Name = "Class D")] classD = 4,
		[Display(Name = "Class E")] classE = 5,
		[Display(Name = "Class F")] classF = 6,
		[Display(Name = "Others")] classOthers = 0
	}

	public class EvidenceDoc
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		[NotNull]
		[StringLength(50)]
		public string? IncidentID { get; set; }
		[Required]
		[NotNull]
		public int IncidentVersion { get; set; } = 0;

		[StringLength(150)]
		public string? LinkAddress { get; set; } = ""; 
		[StringLength(150)]
		public string? FileOnServer { get; set; } = "";
		[StringLength(150)]
		public string? description { get; set; } = " ";
		[Required]
		[NotNull]
		[StringLength(150)]
		public string FileName { get; set; } = " ";
		[StringLength(150)]
		public string? Supplier { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public Guid HSEIncidentsModelId { get; set; }
		//public virtual HSEIncidentsModel HSEIncidentsModel { get; set; }= new HSEIncidentsModel();
	}
	public class DOCFile
	{
		public string content;
		public string newName;

	}
	public class IncidentsDetails
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();
		[Required]
		[NotNull]
		[StringLength(50)]
		public string IncidentID { get; set; } = "?";
		[Required]
		[NotNull]
		public int IncidentVersion { get; set; } = 0;

		public DateTime LastUpdateTime { get; set; } = DateTime.Now;
		[StringLength(30)]
		public string? UpdateBy { get; set; } = "?";
		[Required]
		public CaseStatus Status { get; set; } = 0;
		[Required]
		public string? Discription { get; set; } = "?";
		[Required]
		public string? ImmediateAction { get; set; } = "?";

		public string? Analyzation { get; set; } = "None";
		public string? RootCause { get; set; } = "?";
		public string? Correction { get; set; } = "?";
		public string? FellowUp { get; set; } = "?";
		public string? Approved { get; set; } = "?";
		public Guid HSEIncidentsModelId { get; set; }
		//public virtual HSEIncidentsModel HSEIncidentsModel { get; set; } = new HSEIncidentsModel();

		[Display(Name = "Last Update")]
		public string LastUpdate
		{
			get
			{
				return "Update by:" + UpdateBy + " @ " + LastUpdateTime.ToString("yyyy-MMM-dd HH:mm");
			}
		}
		public IncidentsDetails ShallowCopy()
		{
			return (IncidentsDetails)this.MemberwiseClone();
		}
	}

}
