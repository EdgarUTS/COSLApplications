using COSLApplications.Server.Services.Incident;
using COSLApplications.Shared.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using COSLApplications.Shared.Validations;
using COSLApplications.Server.Date;
using Microsoft.Extensions.Logging;
using System.Net;
using static System.Net.Mime.MediaTypeNames;


namespace COSLApplications.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IncidentController : ControllerBase
	{
		private readonly IWebHostEnvironment _env;
		//private readonly ILogger<IncidentController> logger;

		//public IncidentController(IWebHostEnvironment env,
		//	ILogger<IncidentController> logger)
		//{
		//	this.env = env;
		//	this.logger = logger;
		//}


		private readonly IIncident _case;

		public IncidentController(IIncident icase, IWebHostEnvironment env)
		{
			_case = icase;
			_env = env;
		}
		// GET: api/<UserController>
		[HttpGet]
		public async Task<List<HSEIncidentsModel>> GetAllCases()
		{
			return await _case.GetAllCases();
			//var ss=JsonSerializer.Serialize(res);
			//var ress=System.Text.Json.JsonSerializer.SerializeToDocument<List<HSEIncidentsModel>>(res);
			//return res;
		}
		[HttpGet("maxVersion")]
		public int MaxUserID()
		{
			return _case.MaxCaseVersion();
		}

		[HttpGet("HSE/{id}")]
		public async Task<HSEIncidentsModel> Get(Guid id)
		{
			//return await _personService.GetPerson(id);
			return await _case.GetCase(id);
		}
		[HttpGet("CheckID/{IncidentID}")]
		public async Task<int> Get(string IncidentID)
		{
			var ss= await _case.GetCaseID(IncidentID);
			return ss.Count;
		}
		[HttpGet("DET/{id}/{version}")]
		public async Task<IncidentsDetails> Get(Guid id, int version)
		{
			//return await _personService.GetPerson(id);
			return await _case.GetCaseDetail(id, version);
		}
		[HttpGet("DET/{id}")]
		public async Task<List<IncidentsDetails>> GetDetailList(Guid id)
		{
			//return await _personService.GetPerson(id);
			return await _case.GetAllCasesDetails(id);
		}
		[HttpGet("DOC/{id}")]
		public async Task<List<EvidenceDoc>> GetDocList(Guid id)
		{
			//return await _personService.GetPerson(id);
			return await _case.GetCasesDocs(id);
		}
		[HttpPost("DET")]
		public async Task<IncidentsDetails> AppenduserDetails([FromBody] IncidentsDetails tcase)
		{
			var resp = await _case.AddCase(tcase);
			return resp;
		}
		// POST api/<UserController>
		[HttpPost("HSE")]

		public async Task<HSEIncidentsModel> AppendCaseMain([FromBody] HSEIncidentsModel tcase)
		{
			var resp = await _case.AddCase(tcase);
			return resp;
		}

		[HttpPost("SaveFile")]
		public async Task<EvidenceDoc> AppendDoc([FromBody] EvidenceDoc tcase)
		{
			try
			{
				var resp = await _case.AddCase(tcase);
				return resp;
			}catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}
		[HttpPost("SaveFileAs")]
		public async Task<EvidenceDoc> AppendDocAs([FromBody] EvidenceDoc tcase)
		{
			try
			{
				var resp = await _case.AddCase(tcase);
				if (resp is not null)
				{
					return await _case.UpdateFileName(resp);

					//resp.Link = resp.Link + resp.Id.ToString() + (new FileInfo(resp.FileName)).Extension;
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			return null;
		}
		[HttpPost("UploadLoadFile")]

		//public async Task<ActionResult<UploadResult>> PostFile(
		//List<IFormFile> files)
		//{
		//	var maxAllowedFiles = 1;
		//	long maxFileSize = 1024 * 1024 * 20;
		//	//var filesProcessed = 0;
		//	var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
		//	//List<UploadResult> uploadResults = new();

		//	//foreach (var file in files)
		//	var file = files[0];
		//	var uploadResult = new UploadResult();
		//	string trustedFileNameForFileStorage;
		//	var untrustedFileName = file.FileName;
		//	uploadResult.FileName = untrustedFileName;
		//	var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

		//	if (file.Length == 0)
		//	{
		//		logger.LogInformation("{FileName} length is 0 (Err: 1)",
		//			trustedFileNameForDisplay);
		//		uploadResult.ErrorCode = 1;
		//	}
		//	else if (file.Length > maxFileSize)
		//	{
		//		logger.LogInformation("{FileName} of {Length} bytes is " +
		//			"larger than the limit of {Limit} bytes (Err: 2)",
		//			trustedFileNameForDisplay, file.Length, maxFileSize);
		//		uploadResult.ErrorCode = 2;
		//	}
		//	else
		//	{
		//		try
		//		{
		//			trustedFileNameForFileStorage = Path.GetRandomFileName();
		//			var path = Path.Combine(env.ContentRootPath,
		//				env.EnvironmentName, "HSEDoc",
		//				trustedFileNameForFileStorage);

		//			await using FileStream fs = new(path, FileMode.Create);
		//			await file.CopyToAsync(fs);

		//			logger.LogInformation("{FileName} saved at {Path}",
		//				trustedFileNameForDisplay, path);
		//			uploadResult.Uploaded = true;
		//			uploadResult.StoredFileName = trustedFileNameForFileStorage;
		//		}
		//		catch (IOException ex)
		//		{
		//			logger.LogError("{FileName} error on upload (Err: 3): {Message}",
		//				trustedFileNameForDisplay, ex.Message);
		//			uploadResult.ErrorCode = 3;
		//		}
		//	}
		//	return Ok(uploadResult);
		//}
		////[HttpPost("check")]
		//public async Task<ValidCheck> DataIsExist(ValidCheck vc)
		//{
		//	var v=await _case.DataCheck( "api/Incident",vc);
		//	return v;
		//}

		// PUT api/<UserController>/5
		[HttpPut("{id}")]
		public async Task<bool> UpdateCaseMain(Guid id, [FromBody] HSEIncidentsModel icase)
		{
			await _case.UpdateCase(id, icase);
			return true;
		}

		// DELETE api/<UserController>/5
		[HttpDelete("{id}")]
		public async Task<bool> DeleteAsync(Guid id)
		{
			List<EvidenceDoc> docs =await GetDocList(id);
			if (docs != null)//delete all files on server
			{
				foreach (EvidenceDoc doc in docs)
				{
					string? path = Path.Combine(_env.ContentRootPath, "HSEDoc", doc.FileOnServer);
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
				}
			}
			await _case.DeleteCase(id);
			return true;
		}
	}
}
