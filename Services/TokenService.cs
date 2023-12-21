public class JWTService
{

    private readonly string _secretKey;


    /// <summary>
    /// Génère une clé secrete pour la validation du token
    /// </summary>
    public JWTService()
    {
        // Charger la clé depuis les variables d'environnement
        _secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");

        if (string.IsNullOrEmpty(_secretKey))
        {
            // Si la clé n'est pas définie, générer une nouvelle clé
            _secretKey = GenerateRandomKey();
            // Stocker la nouvelle clé dans les variables d'environnement
            Environment.SetEnvironmentVariable("SECRET_KEY", _secretKey);
        }
    }



    // Si SECRET_KEY est null alors je construit la clé ici
    private string GenerateRandomKey(int length = 32)
    {
        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        var bytes = new byte[length];
        rngCryptoServiceProvider.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }



    /// <summary>
    /// Génère un token avec un id et un role
    /// </summary>
    /// <param name="userId">l'identifiant</param>
    /// <param name="role">la valeur du rôle</param>
    /// <returns>Retourne un token avec un rôle et un identifiant</returns>
    public string GenerateToken(string userId, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role )
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}