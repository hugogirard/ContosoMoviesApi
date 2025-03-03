﻿using Movies.Application.Models;
using System.Runtime.CompilerServices;

namespace MovieApi.Mapping;

public static class ContractMapping
{
    public static Movie MapToMovie(this CreateMovieRequest request) 
    {
        return new Movie
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Genres = request.Genres.ToList(),
            YearOfRelease = request.YearOfRelease,
        };      
    }

    public static MovieResponse MapToResponse(this Movie movie) 
    {
        return new MovieResponse
        {
            Id = movie.Id,
            Slug = movie.Slug,
            Title = movie.Title,
            Genres = movie.Genres,
            YearOfRelease = movie.YearOfRelease
        };
    }

    public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies)
    {
        return new MoviesResponse
        {
            Items = movies.Select(m => m.MapToResponse()).ToList()
        };
    }

    public static Movie MapToMovie(this UpdateMovieRequest request, Guid id)
    {
        return new Movie
        {            
            Id = id,
            Title = request.Title,
            Genres = request.Genres.ToList(),
            YearOfRelease = request.YearOfRelease,
        };
    }
}
