public static void ConfigureAuthorization(this IServiceCollection services)
{

        services.AddAuthorization(options =>
        {
            // Politique pour les utilisateurs avec le rôle "Register"
            options.AddPolicy("RequireRegisterRole", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Register"); // Spécifie le rôle requis
            });

            // Politique pour les utilisateurs avec le rôle "Admin"
            options.AddPolicy("RequireAdminRole", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Admin"); // Spécifie le rôle requis
            });
        });
}