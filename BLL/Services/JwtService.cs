using System.Drawing;
using System.Drawing.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services;

public class JwtService : IJwtService
{

    #region Explication Configuration

    // le configuration permet de récupérer les données du fichier appsettings.json
    //  => on peut récupérer la clef secrète
    //  => on peut récupérer l'issuer
    //  => on peut récupérer l'audience
    // => on peut récupérer le temps d'expiration du token
    // => on peut récupérer le temps d'expiration du refresh token

    #endregion

    
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
        
        // On récupère la clef secrète avec laquelle on encode et on la convertit en UTF8
        
        
        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        // On crée des credentials avec la clef secrète et l'algorithme de hashage
        // Ca sert à encoder le token avec la clef secrète et l'algorithme de hashage choisit


        List<Claim> claims = new List<Claim>() // dictionnaire de clé/valeur concernant un utilisateur particulier (on va injecter ça dans le token)
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
            new Claim(ClaimTypes.Email, user.Email), 
            new Claim(ClaimTypes.Role, user.Role.ToString()) 
        };


        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _configuration["jwt:issuer"],
            Audience = _configuration["jwt:audience"],
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };
        
        
        #region Explication SecurityTokenDescriptor
        
        // TOKEN Basé SUR UN USER GRACE A la list de CLAIMS
        // descriptor car on crée le token avant de l'écrire (=> nous permet d'indentifier l'utilisateur (voir ligne TokenAvance))
        
        // Le SecurityTokenDescriptor sert à définir les informations du token.
        // La différence avec le JWtSecurityToken c'est qu'on a pu ajouter  un subject
        // (qui est un ClaimsIdentity) sur base des claims qu'on a créé
        // On peut donc identifier une personne dans le Token (on peut récupérer l'ID, l'email, le role)
        

        #endregion
        
        
        var handler = new JwtSecurityTokenHandler();
        var tok = handler.CreateToken(descriptor);
        string TokenAvance = handler.WriteToken(tok);

        #region Explication JwtSecurityTokenHandler

        // Un objet JwtSecurityTokenHandler est créé pour créer et écrire le token JWT à partir de SecurityTokenDescriptor.
        //
        //  Enfin, le token JWT est écrit dans une chaîne de caractères et retourné par la méthode GenerateToken.
        //
        // Le token JWT peut être utilisé pour authentifier et autoriser l'utilisateur dans une application .NET.

        #endregion
        

       // Token Vanille des slides du cours => pas de claims  => pas d'infos sur l'utilisateur

        JwtSecurityToken token = new JwtSecurityToken(
            claims: claims,
            issuer: _configuration["jwt:issuer"],
            audience: _configuration["jwt:audience"],
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        
        
        // return new JwtSecurityTokenHandler().WriteToken(token);
        string jwtNature = new JwtSecurityTokenHandler().WriteToken(token);
        
        //return jwtNature;
        
        return TokenAvance;

    }
}
        
#region Explication TokenVanille
// a. Récupérer la clé secrète et la convertir en SymmetricSecurityKey.
//
//     b. Créer des SigningCredentials avec la clé secrète et l'algorithme de hachage (HMAC-SHA256).
//
//     c. Créer un JwtSecurityToken avec les informations suivantes:
// - issuer (émetteur) et audience récupérés depuis la configuration.
// - la date d'expiration (ici, un jour après la création du token).
// - les SigningCredentials créées précédemment.
//
//     d. Retourner le JWT sous forme de chaîne de caractères en utilisant JwtSecurityTokenHandler().WriteToken(token).
//
//     En résumé, ce code crée un service pour générer des JWT avec des informations spécifiques, en utilisant des paramètres de configuration et une clé secrète pour garantir leur authenticité.

// REMARQUE : PAR DEFAUT, LE JWTSECURITYTOKEN N'AJOUTE PAS LES CLAIMS DANS LE TOKEN. IL FAUT LES AJOUTER MANUELLEMENT.
// Ca veut dire que par défaut, on n'a pas d'authentification sur base d'une personne particulière.

#endregion