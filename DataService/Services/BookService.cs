using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AppDbContext.Entity.Entities;
using DataAccessLayer.Models;
using DataService.Interface;

namespace DataService.Services
{
	public class BookService : IBookService
	{
		public async Task<BookModel> GetBookByIdAsync(int? id)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Books
					.Where(x => x.Id == id)
					.Select(x => new BookModel
					{
						Id = x.Id,
						Name = x.Name,
						Synopsis = x.Synopsis,
						AuthorId = x.AuthorId,
						AuthorName = x.Author.FirstName + " " + x.Author.LastName
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<BookModel>> GetAllBooksAsync()
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Books.Select(x => new BookModel
				{
					Name = x.Name,
					Id = x.Id,
					Synopsis = x.Synopsis,
					AuthorId = x.AuthorId,
					AuthorName = x.Author.FirstName + " " + x.Author.LastName
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(BookModel bookToAdd)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				Book bookToAddToDb = new Book
				{
					Name = bookToAdd.Name,
					Synopsis = bookToAdd.Synopsis,
					AuthorId = bookToAdd.AuthorId
				};
				context.Books.Add(bookToAddToDb);
				await context.SaveChangesAsync();

				return bookToAddToDb.Id;
			}
		}
		public async Task<int> UpdateAsync(int id, BookModel model)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				Book entity = new Book
				{
					Id = id,
					AuthorId = model.AuthorId,
					Name = model.Name,
					Synopsis = model.Synopsis
				};
				context.Books.AddOrUpdate(entity);
				await context.SaveChangesAsync();

				return entity.Id;
			}
		}

		public async Task<bool> RemoveBookAsync(int bookId)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				Book entity = context.Books.FirstOrDefault(x => x.Id == bookId);
				if (entity != null)
				{
					context.Books.Remove(entity);
				}
				await context.SaveChangesAsync();
				return true;
			}
		}
	}
}