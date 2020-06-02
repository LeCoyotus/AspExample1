using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataService.Interface
{
	public interface IUserService
	{
		Task<UserModel> GetUserByIdAsync(int id);
		Task<IEnumerable<UserModel>> GetAllUsersAsync();

		Task<int> CreateAsync(UserModel userToAdd);
	}
}