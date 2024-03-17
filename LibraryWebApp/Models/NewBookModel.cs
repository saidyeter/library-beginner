using LibraryDataAccess.Entities;

namespace LibraryWebApp.Models;

public class NewBookModel
{
    public string Name { get; set; }
    public int ReleaseDate { get; set; }
    public int AuthorId { get; set; }
}