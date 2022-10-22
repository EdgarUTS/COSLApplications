using System;
using System.Numerics;
using System.Reflection.Metadata;

using COSLApplications.Server.Date;
using COSLApplications.Server.Services.Incident;
using COSLApplications.Shared.Models;
using COSLApplications.Shared.Validations;

using Microsoft.EntityFrameworkCore;

using static System.Net.Mime.MediaTypeNames;

namespace COSLApplications.Server.Services.Incident
{
	public class IncidentService : IIncident
	{
		readonly ApplicationDbContext _dbContext;
		
		public IncidentService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<HSEIncidentsModel> AddCase(HSEIncidentsModel icase)
		{
			try
			{
				var obj = await _dbContext.HSEIncident.AddAsync(icase);
				_dbContext.SaveChanges();
				return obj.Entity;
			}
			catch (Exception e)
			{ Console.WriteLine(e.Message); }
			return null;
		}

		public async Task<EvidenceDoc> AddCase(EvidenceDoc icase)
		{
			try
			{
				var obj = await _dbContext.EvidenceDoc.AddAsync(icase);
				//_dbContext.Entry(icase.HSEIncidentsModel).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return obj.Entity;
			}catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}

		public async Task<IncidentsDetails> AddCase(IncidentsDetails icase)
		{
			try
			{
				//_dbContext.ChangeTracker.TrackGraph(icase,node=>node.Entry.State=!node.Entry.IsKeySet?EntityState.Added:EntityState.Unchanged);
				//var obj = await _dbContext.IncidentsDetails.AddAsync(icase);
				//_dbContext.Entry(icase.HSEIncidentsModel).State = EntityState.Modified;
				var obj = await _dbContext.IncidentsDetails.AddAsync(icase);
				//_dbContext.Entry(icase.HSEIncidentsModel).State = EntityState.Modified;
				_dbContext.SaveChanges();
				return obj.Entity;
				}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}

		public async Task<ValidCheckClass> DataCheck(string dbc, ValidCheckClass vc)
		{
			int exist = 0;
			string q = "";
			string? db = _dbContext.HSEIncident.EntityType.GetTableName();
			if (vc.command == "unique")
			{
				if (vc.dataType == "string")
				{
					q = $"Select * from [{db}] where UPPER({vc.fieldName})=UPPER('{vc.dataValue}')";
				}
				else
				{
					q = $"Select * from [{db}] where {vc.fieldName} = {vc.dataValue}";
				}
			}
			try
			{
				exist = _dbContext.User.FromSqlRaw(q).ToList().Count;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			if (exist == 0)
				vc.result = "no";
			else
				vc.result = "yes";

			return vc;
		}

		public async Task<bool> DeleteCase(Guid id)
		{
			var data = _dbContext.HSEIncident.FirstOrDefault(x => x.Id == id);
			_dbContext.Remove(data);
			await _dbContext.SaveChangesAsync();
			return true;
		}
		public async Task<List<HSEIncidentsModel>> GetAllCases()
		{
			return await _dbContext.HSEIncident.ToListAsync();
			//var res = await _dbContext.HSEIncident.Include(e => e.IncidentsDetails).ToListAsync();
			//return res;
		}

		public async Task<List<IncidentsDetails>> GetAllCasesDetails(Guid id)
		{
			return await _dbContext.IncidentsDetails.Where(e => e.HSEIncidentsModelId == id).ToListAsync();
		}

		public async Task<HSEIncidentsModel> GetCase(Guid id)
		{
			return await _dbContext.HSEIncident.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<List<HSEIncidentsModel>> GetCaseID(string id)
		{
			return await _dbContext.HSEIncident.Where(x => x.IncidentID.Trim() == id.Trim()).ToListAsync();
		}

		public async Task<IncidentsDetails> GetCaseDetail(Guid id, int version)
		{
			return await _dbContext.IncidentsDetails.Where(e => e.HSEIncidentsModelId == id).FirstOrDefaultAsync(x => x.IncidentVersion == version);

		}

		public async Task<List<EvidenceDoc>> GetCasesDocs(Guid id)
		{
			try
			{
				return await _dbContext.EvidenceDoc.Where(e => e.HSEIncidentsModelId == id).ToListAsync();
			}catch(Exception e)
			{
				Console.WriteLine(e.Message);
				return new List<EvidenceDoc>();
			}
		}

		public int MaxCaseVersion()
		{
			int max = 0;
			try
			{
				max = _dbContext.HSEIncident.Max(p => p.IncidentVersion);
			}
			catch (Exception) { }
			return max;
		}
		public async Task<bool> UpdateCase(Guid id, HSEIncidentsModel icase)
		{
			HSEIncidentsModel? _case = await _dbContext.HSEIncident.FirstOrDefaultAsync(x => x.Id == id);
			if (_case != null)
			{
				//var names = typeof(UserModel).GetProperties()
				//		.Select(property => property.Name)
				//		.ToArray();
				//var a = user[names[1]];
				Guid iid=_case.Id;
					try
				{
					for (int i = 0; i < typeof(HSEIncidentsModel).GetProperties().Length; i++)
					{
						typeof(HSEIncidentsModel).GetProperties()[i].SetValue(_case,
							typeof(HSEIncidentsModel).GetProperties()[i].GetValue(icase, null));
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				_case.Id = iid;
				try
				{
					_dbContext.HSEIncident.Update(_case);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
				await _dbContext.SaveChangesAsync();
				return true;
			}
			else
				return false;
		}
		public async Task<EvidenceDoc> UpdateFileName(EvidenceDoc icase)
		{
			var _case = await _dbContext.EvidenceDoc.FirstOrDefaultAsync(x => x.Id == icase.Id);
			if (_case != null)
			{
				_case.FileOnServer = icase.FileOnServer;// + icase.Id.ToString() + (new FileInfo(icase.FileName)).Extension;
				try
				{
					_dbContext.EvidenceDoc.Update(_case);
					await _dbContext.SaveChangesAsync();
					return _case;
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
			return null;
		}
	}
}
