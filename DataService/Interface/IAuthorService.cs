using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataService.Interface
{
	public interface IAuthorService
	{
		Task<AuthorModel> GetAuthorByIdAsync(int id);
		Task<IEnumerable<AuthorModel>> GetAllAuthorsAsync();

		Task<int> CreateAsync(AuthorModel authorToAdd);
	}
}