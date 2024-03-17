using LibraryDataAccess;
using LibraryDataAccess.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

var db = app.Services.CreateScope().ServiceProvider.GetService<LibraryDbContext>();
if (db is not null)
{
    db.Database.EnsureCreated();
    if (!db.Books.Any())
    {
        #region Kafka

        var kafka = new Author
        {
            Name = "Franz Kafka",
            Origin = "Prague"
        };
        db.Authors.Add(kafka);
        db.SaveChanges();
        db.AddRange(new Book
        {
            AuthorId = kafka.Id,
            Name = "The Metamorphosis",
            ReleaseDate = 1915,
        }, new Book
        {
            AuthorId = kafka.Id,
            Name = "The Trial",
            ReleaseDate = 1914,
        }, new Book
        {
            AuthorId = kafka.Id,
            Name = "The Stoker",
            ReleaseDate = 1912,
        });
        db.SaveChanges();

        #endregion

        #region Tolstoy

        var tolstoy = new Author
        {
            Name = "Lev Tolstoy",
            Origin = "Russia"
        };
        db.Authors.Add(tolstoy);
        db.SaveChanges();
        db.AddRange(new Book
        {
            AuthorId = tolstoy.Id,
            Name = "War and Peace",
            ReleaseDate = 1869,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "Anna Karenina",
            ReleaseDate = 1878,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "Sevastopol Sketches",
            ReleaseDate = 1855,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "The Death of Ivan Ilyich",
            ReleaseDate = 1886,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "Hadji Murad",
            ReleaseDate = 1912,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "Confession",
            ReleaseDate = 1882,
        }, new Book
        {
            AuthorId = tolstoy.Id,
            Name = "Resurrection",
            ReleaseDate = 1899,
        });
        db.SaveChanges();

        #endregion
        
        #region Stephen King

        var sKing = new Author
        {
            Name = "Stephen King",
            Origin = "US"
        };
        
        db.Authors.Add(sKing);
        db.SaveChanges();
        db.AddRange(new Book
        {
            AuthorId = sKing.Id,
            Name = "Carrie",
            ReleaseDate = 1974,
        }, new Book
        {
            AuthorId = sKing.Id,
            Name = "Different Seasons",
            ReleaseDate = 1982,
        }, new Book
        {
            AuthorId = sKing.Id,
            Name = "The Dark Tower: The Gunslinger",
            ReleaseDate = 1982,
        });
        db.SaveChanges();

        #endregion
    }
}

app.Run();