using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Persistence;
using MusicStore.Repositories.interfaces;
using System.Data;
using System.Runtime.CompilerServices;

namespace MusicStore.Repositories.Implementations;

public class GenreRepository : IGenreRepository
{
    private readonly ApplicationDbContext context;

    public GenreRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<List<Genre>> GetAsync()
    {
        return await context.Genres.ToListAsync();
    }
    public async Task<Genre?> GetAsync(int id)
    {
        var item = await context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        if (item is not null)
            return item;
        else
            throw new InvalidOperationException($"No se encontro el registro con id {id}");
    }
    public async Task<int> AddAsync(Genre genre)
    {
        context.Genres.Add(genre);
        await context.SaveChangesAsync();
        return genre.Id;

    }
    public async Task UpdateAsync(int id, Genre genre)
    {
        var item = await GetAsync(id);
        if (item is not null)
        {
            item.Name = genre.Name;
            item.Status = genre.Status;
            context.Update(item);
            await context.SaveChangesAsync();
        }
        else 
        {
            throw new InvalidOperationException($"No se encontro el registro con el id {id}");
        }

    }
    public async Task DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        if (item is not null)
        {
            context.Genres.Remove(item);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new InvalidOperationException($"No se encontro el registro con el id {id}");
        }

    }

}

