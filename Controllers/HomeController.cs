using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FilmFolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // Replace YourDbContext with your actual DbContext class

        public HomeController(ILogger<HomeController> logger, AppDbContext context) // Dependency injection
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UserRoles = new List<string> { "Role_User", "Role_Admin" };
            var pythonScriptOutput = await RunPythonScriptAsync();
            ViewBag.PythonOutput = pythonScriptOutput;

            // Sadece son 3 filmi çek
            var latestMovies = await _context.Movies.OrderByDescending(m => m.ReleaseYear).Take(3).ToListAsync();

            return View(latestMovies);
        }
        private async Task<string> RunPythonScriptAsync()
        {
            string result = string.Empty;
            await Task.Run(() =>
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "C://Users//user//AppData//Local//Programs//Python//Python312//python.exe", // Python interpreter yolu
                    Arguments = "C:/Users/user/Desktop/kNN_Model.py", // Python script yolu
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                using (var process = Process.Start(start))
                {
                    using (var reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                }
            });

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}