﻿namespace MovieApi;

public static class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Movies
    {
        public const string Base = $"{ApiBase}/movies";

        public const string Create = Base;

        public const string Get = $"{Base}/{{id:guid}}";

        public const string Getall = Base;

        public const string Update = $"{Base}/{{id:guid}}";

        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
