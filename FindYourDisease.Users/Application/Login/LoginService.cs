using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.DTO;
using FindYourDisease.Users.Domain.Options;
using FindYourDisease.Users.Infra.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureIdentity.Password;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindYourDisease.Users.Application.Login;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly JwtOptions _jwtOptions;
    private readonly INotificationCollector _notification;

    public LoginService(IUserRepository userRepository, IOptions<JwtOptions> options, INotificationCollector notification)
    {
        _userRepository = userRepository;
        _jwtOptions = options.Value;
        _notification = notification;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetAsync("Email", request.Email);

        if (user is null || !PasswordHasher.Verify(user.HashedPassword, request.Password))
        {
            _notification.AddNotification(ErrorMessages.User_Not_Found);
            return default;
        }

        var token = GenerateToken(user);

        return token;
    }

    private string GenerateToken(Domain.Model.User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Locality, user.Localization())
            }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.LifeTimeMinutes),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
