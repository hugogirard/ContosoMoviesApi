﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Movies.Application.Models;

public class Movie
{
    public required Guid Id { get; init; }

    public required string Title { get; set; }

    public string Slug => GenerateSlug();

    public required int YearOfRelease { get; set; }

    public required List<string> Genres { get; init; } = new();

    public string GenerateSlug() 
    {
        var sluggedTitle = Regex.Replace(Title, "[^0-9A-Za-z _-]", string.Empty)
                                .ToLower().Replace(" ","-");

        return $"{sluggedTitle}-{YearOfRelease}";
    }
}
