using LibraryDataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryDataAccess;

public class LibraryDbContext:DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source=library.db");
    }
}