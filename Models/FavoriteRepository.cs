using FilmFolio.Utility;
using Microsoft.EntityFrameworkCore;

namespace FilmFolio.Models
{
    public class FavoriteRepository : Repository<Favorite> , IFavoriteRepository
    {
        private AppDbContext _appDbContext;
        public FavoriteRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public async Task AddToFavorites(Favorite favorite)
        {
            _appDbContext.FavoriteList.Add(favorite);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId)
        {
            return await _appDbContext.FavoriteList
                .Where(f => f.IdentityUserId == userId)
                .Include(f => f.Movie)
                .ToListAsync();
        }
    }
}
