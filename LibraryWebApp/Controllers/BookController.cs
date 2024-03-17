using System.Diagnostics;
using LibraryDataAccess;
using LibraryDataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;

namespace LibraryWebApp.Controllers;

public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly LibraryDbContext _libraryDbContext;

    public BookController(ILogger<BookController> logger, LibraryDbContext libraryDbContext)
    {
        _logger = logger;
        _libraryDbContext = libraryDbContext;
    }

    public IActionResult Index()
    {
        var books = _libraryDbContext.Books.ToList();
        var authors = _libraryDbContext.Authors.ToList();

        var data = new List<BookViewModel>();
        foreach (var book in books)
        {
            data.Add(new BookViewModel()
            {
                Name = book.Name,
                AuthorId = book.AuthorId,
                ReleaseDate = book.ReleaseDate,
                Id = book.Id,
                AuthorName = authors.First(x => x.Id == book.AuthorId).Name,
            });
        }

        return View(data.ToArray());
    }

    [HttpGet("book/single/{id}")]
    public IActionResult SingleBook(int id)
    {
        var book = _libraryDbContext.Books.FirstOrDefault(x => x.Id == id);
        if (book is null)
        {
            ViewBag.Title = "Not found";
            return View(null);
        }

        ViewBag.Title = book.Name;

        var authorName = _libraryDbContext.Authors.First(x => x.Id == book.AuthorId).Name;
        return View(new BookViewModel
        {
            Name = book.Name,
            AuthorId = book.AuthorId,
            ReleaseDate = book.ReleaseDate,
            Id = book.Id,
            AuthorName = authorName,
        });
    }

    [HttpGet(Name = "/create")]
    public IActionResult Create()
    {
        var authors = _libraryDbContext.Authors.ToArray();
        if (authors.Length == 0)
        {
            ViewBag.Error = "Author is not found";
            return null;
        }

        ViewBag.Error = string.Empty;
        ViewBag.Authors = authors;
        return View(new NewBookModel()
        {
            AuthorId = authors.FirstOrDefault().Id,
            ReleaseDate = 2000,
            Name = ""
        });
    }

    [HttpPost(Name = "/create")]
    public IActionResult Create(NewBookModel newBookModel)
    {
        Console.WriteLine("book cre");
        var author = _libraryDbContext.Authors.FirstOrDefault(x => x.Id == newBookModel.AuthorId);
        if (author is null)
        {
            var authors = _libraryDbContext.Authors.ToArray();
            if (authors.Length == 0)
            {
                ViewBag.Error = "Author is not found";
                return null;
            }

            ViewBag.Error = "Author is not found";
            ViewBag.Authors = authors;


            return View(newBookModel);
        }

        _libraryDbContext.Add(new Book()
        {
            AuthorId = newBookModel.AuthorId,
            ReleaseDate = newBookModel.ReleaseDate,
            Name = newBookModel.Name
        });
        _libraryDbContext.SaveChanges();

        return RedirectToAction("Index", "Book");
    }
}