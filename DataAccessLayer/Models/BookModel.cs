namespace DataAccessLayer.Models
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Synopsis { get; set; }
		public int AuthorId { get; set; }
		public string AuthorName { get; set; }
	}
}