using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components.Forms;

namespace COSLApplications.Server.Pages.Funcs
{
	//https://learn.microsoft.com/en-us/aspnet/core/blazor/file-uploads?view=aspnetcore-6.0&pivots=server
	public class UploadFiles
	{
		private List<File> files = new();
		private List<UploadResult> uploadResults = new();
		private bool shouldRender;
		
		/// <summary>
		/// /protected override bool ShouldRender() => shouldRender;
		/// </summary>
		private readonly IHttpClientFactory ClientFactory;
		private readonly ILogger<FileUpload2> Logger;

		private async Task OnInputFileChange(InputFileChangeEventArgs e)
		{
			shouldRender = false;
			long maxFileSize = 1024 * 15;
			var upload = false;
			int maxAllowedFiles = 10;
			using var content = new MultipartFormDataContent();

			foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
			{
				if (uploadResults.SingleOrDefault(
					f => f.FileName == file.Name) is null)
				{
					try
					{
						var fileContent =
							new StreamContent(file.OpenReadStream(maxFileSize));

						fileContent.Headers.ContentType =
							new MediaTypeHeaderValue(file.ContentType);

						files.Add(new() { Name = file.Name });

						content.Add(
							content: fileContent,
							name: "\"files\"",
							fileName: file.Name);

						upload = true;
					}
					catch (Exception ex)
					{
						Logger.LogInformation(
						    "{FileName} not uploaded (Err: 6): {Message}", 
						    file.Name, ex.Message);

						uploadResults.Add(
							new()
							{
								FileName = file.Name,
								ErrorCode = 6,
								Uploaded = false
							});
					}
				}
			}

			if (upload)
			{
				var client = ClientFactory.CreateClient();

				var response =
					await client.PostAsync("https://localhost:5001/Filesave",
					content);

				if (response.IsSuccessStatusCode)
				{
					var options = new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true,
					};

					using var responseStream =
						await response.Content.ReadAsStreamAsync();

					var newUploadResults = await JsonSerializer
						.DeserializeAsync<IList<UploadResult>>(responseStream, options);

					if (newUploadResults is not null)
					{
						uploadResults = uploadResults.Concat(newUploadResults).ToList();
					}
				}
			}

			shouldRender = true;
		}

		public class UploadResult
		{
			public bool Uploaded { get; set; }
			public string? FileName { get; set; }
			public string? StoredFileName { get; set; }
			public int ErrorCode { get; set; }
		}

		private static bool FileUpload(IList<UploadResult> uploadResults,
			string? fileName, ILogger<FileUpload2> logger, out UploadResult result)
		{
			result = uploadResults.SingleOrDefault(f => f.FileName == fileName) ?? new();

			if (!result.Uploaded)
			{
				logger.LogInformation("{FileName} not uploaded (Err: 5)", fileName);
				result.ErrorCode = 5;
			}

			return result.Uploaded;
		}

		private class File
		{
			public string? Name { get; set; }
		}

	}




}
