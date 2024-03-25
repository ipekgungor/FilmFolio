using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FilmFolio.Models;
using System.Threading.Tasks;
using System.Linq;
using FilmFolio.Utility;

public class ProfileController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context; // Assuming you have a DbContext for database access

    public ProfileController(UserManager<IdentityUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return View("Error");
        }

        var appUser = user as ApplicationUser;
        ViewBag.Username = user.UserName;
        ViewBag.BirthDate = appUser.BirthDate;
        ViewBag.Gender =appUser.Gender;
        ViewBag.ProfileImageUrl = appUser?.ImageURL;

        var favorites = _context.FavoriteList.Where(f => f.IdentityUserId == user.Id)
                                          .Select(f => f.Movie)
                                          .ToList();

        ViewBag.Watchlist = favorites;

        return View();

        //edit profile fonk.
    }
}
