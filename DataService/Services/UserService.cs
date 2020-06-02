using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataService.Interface;
using AppDbContext.Entity.Entities;

namespace DataService.Services
{
	public class UserService : IUserService
	{
		public async Task<UserModel> GetUserByIdAsync(int id)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Users
					.Where(x => x.Id == id)
					.Select(x => new UserModel
					{
						Username = x.UserName,
						Password = x.PassWord,
						Right = (int)x.Right
					}).FirstOrDefaultAsync();
			}
		}

		public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				return await context.Users.Select(x => new UserModel
				{
					Username = x.UserName,
					Password = x.PassWord,
					Right = (int)x.Right
				}).ToListAsync();
			}
		}

		public async Task<int> CreateAsync(UserModel userToAdd)
		{
			using (ApplicationDbContext context = new ApplicationDbContext())
			{
				User userToAddToDb = new User
				{
					UserName = userToAdd.Username,
					PassWord = userToAdd.Password,
					Right = (Right)userToAdd.Right
				};
				context.Users.Add(userToAddToDb);
				await context.SaveChangesAsync();

				return userToAddToDb.Id;
			}
		}
	}
}