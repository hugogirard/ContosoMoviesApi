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
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie.MapToResponse());        
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

    [HttpGet(ApiEndpoints.Movies.Getall)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieRepository.GetAllAsync();
        return Ok(movies.MapToResponse());
    }

    [HttpPut(ApiEndpoints.Movies.Update)]
    public async Task<IActionResult> UpdateMovie([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
    {
        var movie = request.MapToMovie(id);
        var updated = await _movieRepository.UpdateAsync(movie);

        if (!updated)
        {
            return NotFound();
        }

        var response = movie.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Movies.Delete)]
    public async Task<IActionResult> DeleteMovie([FromRoute] Guid id)
    {
        var deleted = await _movieRepository.DeleteByIdAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok();
    }
}
