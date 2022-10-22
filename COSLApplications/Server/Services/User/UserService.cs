using System;
using System.Reflection.Metadata;

using COSLApplications.Server.Date;
using COSLApplications.Shared.Models;
using COSLApplications.Shared.Validations;

using Microsoft.EntityFrameworkCore;

namespace COSLApplications.Server.Services.User
{
	public class UserService : IUser
	{
		readonly ApplicationDbContext _dbContext;

		public UserService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<UserModel> AddUser(UserModel user)
		{
			var obj = await _dbContext.User.AddAsync(user);
			_dbContext.SaveChanges();
			return obj.Entity;
		}

		public async Task<ValidCheckClass> DataCheck(string dbc, ValidCheckClass vc)
		{
			int exist = 0;
			string q = "";
			string db = _dbContext.User.EntityType.GetTableName();
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

		public async Task<bool> DeleteUser(Guid id)
		{
			var data = _dbContext.User.FirstOrDefault(x => x.Id == id);
			_dbContext.Remove(data);
			await _dbContext.SaveChangesAsync();
			return true;

		}
		public async Task<List<UserModel>> GetAllUsers()
		{
			return await _dbContext.User.ToListAsync();
		}

		public async Task<UserModel> GetUser(Guid id)
		{
			return await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
		}
		public int MaxUserID()
		{
			int max = 0;
			try
			{
				max = _dbContext.User.Max(p => p.UserID);
			}
			catch (Exception) { }
			return max;
		}

		public async Task<bool> UpdateUser(Guid id, UserModel user)
		{
			var _user = await _dbContext.User.FirstOrDefaultAsync(x => x.Id == id);
			if (_user != null)
			{
				//var names = typeof(UserModel).GetProperties()
				//		.Select(property => property.Name)
				//		.ToArray();
				//var a = user[names[1]];
				for (int i = 1; i < typeof(UserModel).GetProperties().Length; i++)
				{
					typeof(UserModel).GetProperties()[i].SetValue(_user,
						typeof(UserModel).GetProperties()[i].GetValue(user, null));
				}
				try
				{
					_dbContext.User.Update(_user);
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
	}
}
