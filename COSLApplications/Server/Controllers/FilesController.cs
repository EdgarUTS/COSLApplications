using System.IO;
using System.Net;

using COSLApplications.Shared.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using static COSLApplications.Server.Pages.Funcs.UploadFiles;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using UploadResult = COSLApplications.Shared.Models.UploadResult;

namespace COSLApplications.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class FilesController : ControllerBase
	{
		//private readonly IFileService _fileService;
		private const string MimeType = "image/png";
		private const string FileName = "CM-Logo.png";

		private readonly IWebHostEnvironment env;
		//private readonly ILogger<FilesaveController> logger;

		public FilesController(IWebHostEnvironment env)//,ILogger<FilesaveController> logger)
		{
			this.env = env;

			//this.logger = logger;
		}
		[HttpGet("{fileName}")]
		public async Task<IActionResult> DownloadFile(string fileName)
		{
			var path = Path.Combine(env.ContentRootPath, "HSEDoc", fileName);
			var memory = new MemoryStream();
			using (var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
			{ await stream.CopyToAsync(memory); }
			memory.Position = 0;
			return File(memory,"application/text",fileName);

		}
		[HttpPost, DisableRequestSizeLimit]
		public async Task<ActionResult<UploadResult>> PostFile(List<IFormFile> files)
		{
			var maxAllowedFiles = 3;
			long maxFileSize = 1024 * 1024 * 20;
			var filesProcessed = 0;
			var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
			List<UploadResult> uploadResults = new();

			foreach (var file in files)
			{
				var uploadResult = new UploadResult();
				string trustedFileNameForFileStorage;
				var untrustedFileName = file.FileName;
				uploadResult.FileName = untrustedFileName;
				var trustedFileNameForDisplay =
					WebUtility.HtmlEncode(untrustedFileName);

				try
				{
					trustedFileNameForFileStorage = Path.GetRandomFileName() + "__()__" + file.FileName;
					var path = Path.Combine(env.ContentRootPath, //AppDomain.CurrentDomain.BaseDirectory,//env.ContentRootPath,
						@"HSEDoc",
						trustedFileNameForFileStorage);

					await using FileStream fs = new(path, FileMode.Create);
					await file.CopyToAsync(fs);

					//logger.LogInformation("{FileName} saved at {Path}",
					//	trustedFileNameForDisplay, path);
					uploadResult.Uploaded = true;
					uploadResult.StoredFileName = trustedFileNameForFileStorage;
				}
				catch (IOException ex)
				{
					//logger.LogError("{FileName} error on upload (Err: 3): {Message}",
					//	trustedFileNameForDisplay, ex.Message);
					uploadResult.ErrorCode = 3;
					return BadRequest(ex.Message);
				}
				uploadResults.Add(uploadResult);
			}
			return Ok(uploadResults);
		}
	}
}
