using COSLApplications.Shared.Models;
using COSLApplications.Shared.Validations;

namespace COSLApplications.Server.Services.Incident
{
	public interface IIncident
	{
		Task<HSEIncidentsModel> AddCase(HSEIncidentsModel theCase);
		Task<EvidenceDoc> AddCase(EvidenceDoc theCase);
		Task<IncidentsDetails> AddCase(IncidentsDetails theCase);
		Task<bool> UpdateCase(Guid id, HSEIncidentsModel theCase);
		Task<bool> DeleteCase(Guid id);
		Task<List<EvidenceDoc>> GetCasesDocs(Guid id);
		Task<List<IncidentsDetails>> GetAllCasesDetails(Guid id);
		Task<List<HSEIncidentsModel>> GetAllCases();
		Task<HSEIncidentsModel> GetCase(Guid id);
		Task<List<HSEIncidentsModel>> GetCaseID(string id);
		Task<IncidentsDetails> GetCaseDetail(Guid id, int version);
		int MaxCaseVersion();
		//Task<ValidCheck> DataCheck(string controller, ValidCheck vc);
		Task<EvidenceDoc> UpdateFileName(EvidenceDoc icase);
	}
}
