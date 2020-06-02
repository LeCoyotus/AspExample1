using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AppDbContext.Entity.Entities;
using DataAccessLayer.Models;
using DataService.Interface;
using DataService.Services;
using WebAppEmpty.ViewModels;

namespace WebAppEmpty.Controllers
{
    public class BooksController : Controller
    {
	    private IBookService bookService = new BookService();
        private IAuthorService authorService = new AuthorService();
        // GET: Books
        public async Task<ActionResult> Index()
        {
	        IEnumerable<BookModel> books = await bookService.GetAllBooksAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookModel bookModel = await bookService.GetBookByIdAsync(id);
            if (bookModel == null)
            {
                return HttpNotFound();
            }
            return View(bookModel);
        }

        // GET: Books/Create
        public async Task<ActionResult> Create()
        {
            var addBookVm = new AddBookViewModel
            {
                Authors = await authorService.GetAllAuthorsAsync()
            };
            return View(addBookVm);
        }

        // POST: Books/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Synopsis,AuthorId")] AddBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                BookModel bookToAdd = new BookModel
                {
                    AuthorId = book.AuthorId,
                    Name = book.Name,
                    Synopsis = book.Synopsis
                };
	            await bookService.CreateAsync(bookToAdd);
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookModel book = await bookService.GetBookByIdAsync(id);

            EditBookViewModel bookToEdit = new EditBookViewModel
            {
                AuthorId = book.AuthorId,
                Authors = await authorService.GetAllAuthorsAsync(),
                Id = book.Id,
                Name = book.Name,
                Synopsis = book.Synopsis
            };
            return View(bookToEdit);
        }

        // POST: Books/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Synopsis,AuthorId")] EditBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                BookModel bookToAdd = new BookModel
                {
                    AuthorId = book.AuthorId,
                    Id = book.Id,
                    Name = book.Name,
                    Synopsis = book.Synopsis
                };

                await bookService.UpdateAsync(bookToAdd.Id, bookToAdd);

                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
	        if (id == null)
	        {
		        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
	        }

	        BookModel bookModel = await bookService.GetBookByIdAsync(id);
	        if (bookModel == null)
	        {
		        return HttpNotFound();
	        }
	        return View(bookModel);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
	        await bookService.RemoveBookAsync(id);
            return RedirectToAction("Index");
        }
    }
}
