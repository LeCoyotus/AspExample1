using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataService.Interface
{
	public interface IBookService
	{
		Task<BookModel> GetBookByIdAsync(int? id);
		Task<IEnumerable<BookModel>> GetAllBooksAsync();

		Task<int> CreateAsync(BookModel bookToAdd);
		Task<int> UpdateAsync(int id, BookModel model);
		Task<bool> RemoveBookAsync(int bookId);
	}
}