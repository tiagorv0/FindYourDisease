using FindYourDisease.Users.Application.Enum;
using FindYourDisease.Users.Domain.Model;
using FindYourDisease.Users.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindYourDisease.Users.Application.Service;
public class AuthService : IAuthService
{
    private readonly JwtOptions _jwtOptions;

    public AuthService(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateTokenUser(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, RolePolicy.User)
        };

        return GenerateToken(claims);
    }

    public string GenerateTokenPatient(Patient patient)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Sid, patient.Id.ToString()),
            new Claim(ClaimTypes.Name, patient.Name),
            new Claim(ClaimTypes.Email, patient.Email),
            new Claim(ClaimTypes.MobilePhone, patient.Phone),
            new Claim(ClaimTypes.Role, RolePolicy.Patient)
        };

        return GenerateToken(claims);
    }

    private string GenerateToken(Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
