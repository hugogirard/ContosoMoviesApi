using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace MovieApi.Controllers;

[ApiController]
[Route("api")]
public class MovieController : Controller
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpPost(ApiEndpoints.Movies.Create)]    
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request)
    {        
        var movie = request.MapToMovie();

        await _movieRepository.CreateAsync(movie);
        return Created($"{ApiEndpoints.Movies.Create}/{movie.Id}", movie.MapToResponse());
    }

    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie is null)
        {
            return NotFound();
        }
        return Ok(movie.MapToResponse());
    }
}
