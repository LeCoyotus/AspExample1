namespace AppDbContext.Entity.Entities
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext()
			: base("name=ApplicationDbContext")
		{
		}

		public virtual DbSet<Author> Authors { get; set; }
		public virtual DbSet<Book> Books { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Author>()
				.HasMany(e => e.Books)
				.WithRequired(e => e.Author)
				.HasForeignKey(e => e.AuthorId);
		}
	}
}
