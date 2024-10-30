using MusicStore.Entities;

namespace MusicStore.Repositories.interfaces
{
    public interface IGenreRepository
    {
        Task<int> AddAsync(Genre genre);
        Task DeleteAsync(int id);
        Task<List<Genre>> GetAsync();
        Task<Genre?> GetAsync(int id);
        Task UpdateAsync(int id, Genre genre);
    }
}