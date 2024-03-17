using System.Diagnostics;
using LibraryDataAccess;
using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;

namespace LibraryWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LibraryDbContext _libraryDbContext;

    public HomeController(ILogger<HomeController> logger,LibraryDbContext libraryDbContext)
    {
        _logger = logger;
        _libraryDbContext = libraryDbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}