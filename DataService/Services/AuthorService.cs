using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AppDbContext.Entity.Entities;
using DataAccessLayer.Models;
using DataService.Interface;

namespace DataService.Services
{
	public class AuthorService : IAuthorService
	{
		public async Task<AuthorModel> GetAuthorByIdAsync(int id)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Authors
					.Where(x => x.Id == id)
					.Select(x => new AuthorModel
					{
						Id = x.Id,
						FirstName = x.FirstName,
						LastName = x.LastName,
						DateOfBirth = x.DateOfBirth
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<AuthorModel>> GetAllAuthorsAsync()
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Authors.Select(x => new AuthorModel
				{
					Id = x.Id,
					FirstName = x.FirstName,
					LastName = x.LastName,
					DateOfBirth = x.DateOfBirth
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(AuthorModel authorToAdd)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				Author authorToAddToDb = new Author
				{
					FirstName = authorToAdd.FirstName,
					LastName = authorToAdd.LastName,
					DateOfBirth = authorToAdd.DateOfBirth
				};
				context.Authors.Add(authorToAddToDb);
				await context.SaveChangesAsync();

				return authorToAddToDb.Id;
			}
		}
	}
}