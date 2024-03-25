namespace FilmFolio.Models
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task AddToFavorites(Favorite favorite);
        Task<IEnumerable<Favorite>> GetFavoritesByUserId(string userId);
        void Save();
    }
}
