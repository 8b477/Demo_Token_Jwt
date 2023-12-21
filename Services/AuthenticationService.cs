public static class AddAuthenticationService
{
    /// <summary>
    /// Configure les services d'authentification pour utiliser l'authentification JWT (JSON Web Tokens).
    /// </summary>
    /// <param name="services">La collection de services.</param>

    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = Environment.GetEnvironmentVariable("SECRET_KEY"),
                        //ValidIssuer = "http://localhost",
                        //ValidAudience = "http://localhost"
                    };
                });
    }
}