using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
namespace COSLApplications.Shared.Validations
{
	public class DataExist
	{
		public async Task<string> dataExist(string controller, string fieldName, string content)
		{
			var httpClient = new HttpClient();

			var response = await httpClient.GetStringAsync(string.Format("api/User/unique/{0}/{1}", fieldName, content));
			return response;
		}
	}
}
