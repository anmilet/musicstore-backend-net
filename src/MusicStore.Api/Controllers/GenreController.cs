using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.interfaces;
using System.Net;

namespace MusicStore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreRepository repository;
    private readonly ILogger<GenreController> logger;

    public GenreController(IGenreRepository repository, ILogger<GenreController> logger)
    {
        this.repository = repository;
        this.logger = logger;
    }
    // GenreRepository repository = new();

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = new BaseResponseGeneric<ICollection<Genre>>();
        try
        {
            response.Data = await repository.GetAsync();
            response.Success = true;
            logger.LogInformation($"Obteniendo todos los generos musicales");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información.";
            logger.LogError(ex, $"{response.ErrorMessage}{ex.Message}");
            return BadRequest(response);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = new BaseResponseGeneric<Genre>();
        try
        {
            response.Data = await repository.GetAsync(id);
            response.Success = true;
            logger.LogInformation($"Obteniendo todos los generos musicales con id {id}");
            return response.Data is not null ? Ok(response):NotFound();
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información por id.";
            logger.LogError(ex, $"{response.ErrorMessage}{ex.Message}");
            return BadRequest(response);
        }
    }

    [HttpPost()]
    public async Task<IActionResult> Post(Genre genre)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            await repository.AddAsync(genre);
            response.Data = genre.Id;
            response.Success = true;
            logger.LogInformation($"Generos musicales insertado con id {genre.Id}");
            return StatusCode((int)HttpStatusCode.Created, response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al insertar.";
            logger.LogError(ex, $"{response.ErrorMessage}{ex.Message}");
            return BadRequest(response);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Genre genre)
    {
        var response = new BaseResponse();
        try
        {
            await repository.UpdateAsync(id, genre);
            response.Success = true;
            logger.LogInformation($"Generos musicales con id {id} actualizado");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al actualizar.";
            logger.LogError(ex, $"{response.ErrorMessage}{ex.Message}");
            return BadRequest(response);
        }
        
    }

    [HttpDelete("id:int")]
    public async Task<IActionResult> Delete(int id)
    {

        var response = new BaseResponse();
        try
        {
            await repository.DeleteAsync(id);
            response.Success = true;
            logger.LogInformation($"Generos musicales con id {id} eliminado.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al eliminar.";
            logger.LogError(ex, $"{response.ErrorMessage}{ex.Message}");
            return BadRequest(response);
        }
       
    }
}

