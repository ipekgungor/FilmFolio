using FilmFolio.Models;
using FilmFolio.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace FilmFolio.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepo;
        private readonly IMovieRepository _movieRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public FavoriteController(IFavoriteRepository favoriteRepo, IMovieRepository movieRepo, UserManager<IdentityUser> userManager)
        {
            _favoriteRepo = favoriteRepo;
            _movieRepo = movieRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult MovieAddEdit()
        {
            var movieList = _movieRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.MovieName,
                Value = u.Id.ToString()
            });

            ViewBag.MovieList = movieList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MovieAddEdit([Bind("MovieID")] Favorite favorite)
        {
                favorite.IdentityUserId = _userManager.GetUserId(User);
                await _favoriteRepo.AddToFavorites(favorite); 
                return RedirectToAction("Index");

            /*ViewBag.MovieList = _movieRepo.GetAll().Select(u => new SelectListItem
            {
                Text = u.MovieName,
                Value = u.Id.ToString()
            }).ToList();
            return View(favorite);*/
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User); // Get the current user's ID
            var favorites = await _favoriteRepo.GetFavoritesByUserId(userId); // Get the favorites for the user
            return View(favorites); // Pass the favorites to the view
        }
    }
}
