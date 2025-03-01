using Microsoft.AspNetCore.Mvc;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contract.Requests;
using Movies.Contract.Responses;

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

    [HttpPost("movies")]    
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request)
    {
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Genres = request.Genres.ToList(),
            YearOfRelease = request.YearOfRelease,
        };
        
        await _movieRepository.CreateAsync(movie);

        return Ok(new MovieResponse
        { 
            Id = movie.Id,
            Title = movie.Title,
            Genres = movie.Genres,
            YearOfRelease = movie.YearOfRelease
        });
    }
}
