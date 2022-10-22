using System.Reflection.Emit;
using System.Reflection.Metadata;

using COSLApplications.Shared.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace COSLApplications.Server.Date
{
	public partial class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public Microsoft.EntityFrameworkCore.DbSet<UserModel> User { get; set; }
		public Microsoft.EntityFrameworkCore.DbSet<HSEIncidentsModel> HSEIncident { get; set; }
		public DbSet<EvidenceDoc> EvidenceDoc { get; set; }
		public DbSet<IncidentsDetails> IncidentsDetails { get; set; }
		public DbSet<Training> Training { get; set; }
		//protected override void OnModelCreating(DbModelBuilder modelBuilder)
		//{
		//	throw new UnintentionalCodeFirstException();
		//}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserModel>(entity =>
			{
				//		entity.ToTable("userdetailss");
				//		entity.Property(e => e.Userid).HasColumnName("Userid");
				//		entity.Property(e => e.Username)
				//			.HasMaxLength(100)
				//			.IsUnicode(false);
				//		entity.Property(e => e.Address)
				//			.HasMaxLength(500)
				//			.IsUnicode(false);
				//		entity.Property(e => e.Cellnumber)
				//			.HasMaxLength(50)
				//			.IsUnicode(false);
				//		entity.Property(e => e.Emailid)
				//			.HasMaxLength(50)
				//			.IsUnicode(false);
				//		entity.HasIndex(e => e.Emailid).IsUnique();
				//		entity.HasAlternateKey(u => u.Emailid);
				//		modelBuilder.Entity<UserModel>().HasAlternateKey(u => u.UserID);
			});
			//modelBuilder.Entity<HSEIncidentsModel>()
	  // .HasMany(c => c.EvidenceDoc);
	  // //.WithOne(e => e.HSEIncidentsModel);
			//modelBuilder.Entity<HSEIncidentsModel>()
	  // .HasMany(c => c.IncidentsDetails);
			//modelBuilder.Entity<HSEIncidentsModel>(entity =>
			//{
			//	modelBuilder.Entity<HSEIncidentsModel>().Property(b => b.CreateTime)
			//.HasDefaultValue(DateTime.Now);
			//});
			OnModelCreatingPartial(modelBuilder);
		}
		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
		//protected override System.Data.Entity.Validation.DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, System.Collections.Generic.IDictionary<object, object> items) { }
		//protected override DbEntityValidationResult ValidateEntity(System.Data.Entity.Infrastructure.DbEntityEntry entityEntry, IDictionary<object, object> items)
		//{
		//	// create our customized result to add a possible DbValidationError to it
		//	var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());

		//	// We need to check for duplication just in case of adding new entities or modifing existed ones
		//	if (entityEntry.State == EntityState.Added)
		//		checkDuplication(entityEntry, result);

		//	if (entityEntry.State == EntityState.Modified)
		//		checkDuplication(entityEntry, result);


		//	// After we did our check to the entity, if we found any duplication, we don't want to continue
		//	// so we just return our DbEntityValidationResult
		//	if (!result.IsValid)
		//		return result;

		//	// If we didn't find and duplications, then let DbContext do its ordinary checks
		//	return base.ValidateEntity(entityEntry, items);
		//}
	}
}
