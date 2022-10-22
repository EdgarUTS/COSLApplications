using System;
using System.Collections.Generic;

using COSLApplications.Server.Services.User;
using COSLApplications.Shared.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using COSLApplications.Shared.Validations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace COSLApplications.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUser _user;
		public UserController(IUser user)
		{
			_user = user;
		}
		// GET: api/<UserController>
		[HttpGet]
		public async Task<List<UserModel>> GetAllUser()
		{
			return await _user.GetAllUsers();
		}
		[HttpGet("maxUserID")]
		public int MaxUserID()
		{
			return _user.MaxUserID();
		}
		//[HttpGet("unique1/{fieldName}/{content}")]
		//public bool DataIsExist(string fielName, string content)
		//{
		//	var a =  _user.DataIsExist(fielName, content);
		//	return true;
		//}
		//[HttpGet("unique/{fieldName}/{content}")]
		//public async Task DataIsExist1(string fieldName, string content)
		//{
		//	var a =  _user.DataCheck(fieldName, content);
		//	return true;
		//}
		// GET api/<UserController>/5
		[HttpGet("{id}")]
		public async Task<UserModel> Get(Guid id)
		{
			//return await _personService.GetPerson(id);
			return await _user.GetUser(id);
		}

		// POST api/<UserController>
		[HttpPost]
		public async Task<UserModel> Appenduser([FromBody] UserModel user)
		{
			return await _user.AddUser(user);
		}
		[HttpPost("check")]
		public async Task<ValidCheckClass> DataIsExist(ValidCheckClass vc)
		{
			var v = await _user.DataCheck("api/User", vc);
			return v;
		}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public async Task<bool> UpdateUser(Guid id, [FromBody] UserModel user)
		{
			await _user.UpdateUser(id, user);
			return true;
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public async Task<bool> DeleteAsync(Guid id)
		{
			await _user.DeleteUser(id);
			return true;
		}
	}
}
