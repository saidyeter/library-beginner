using LibraryDataAccess.Entities;

namespace LibraryWebApp.Models;

public class BookViewModel :Book
{
    public string AuthorName { get; set; }
}