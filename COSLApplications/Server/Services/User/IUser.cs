using COSLApplications.Shared.Models;
using COSLApplications.Shared.Validations;

namespace COSLApplications.Server.Services.User
{
	public interface IUser
	{
		Task<UserModel> AddUser(UserModel user);
		Task<bool> UpdateUser(Guid id, UserModel user);
		Task<bool> DeleteUser(Guid id);
		Task<List<UserModel>> GetAllUsers();
		Task<UserModel> GetUser(Guid id);
		int MaxUserID();
		Task<ValidCheckClass> DataCheck(string controller, ValidCheckClass vc);
	}
}
