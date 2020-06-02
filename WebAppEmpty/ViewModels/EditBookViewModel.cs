using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models;

namespace WebAppEmpty.ViewModels
{
	public class EditBookViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter name")]
		[MaxLength(30), MinLength(3)]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please resume the book")]
		[MaxLength(300), MinLength(10)]
		public string Synopsis { get; set; }
		public int AuthorId { get; set; }
		public IEnumerable<AuthorModel> Authors { get; set; }
	}
}