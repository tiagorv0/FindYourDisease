using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Options;
using FindYourDisease.Patient.Repository;
using FindYourDisease.Patient.Service;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SecureIdentity.Password;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindYourDisease.Patient.Application.Login;

public class LoginService : ILoginService
{
    private readonly IPatientRepository _patientRepository;
    private readonly JwtOptions _jwtOptions;
    private readonly INotificationCollector _notification;

    public LoginService(IPatientRepository patientRepository, IOptions<JwtOptions> options, INotificationCollector notification)
    {
        _patientRepository = patientRepository;
        _jwtOptions = options.Value;
        _notification = notification;
    }

    public async Task<string> LoginAsync(LoginRequest request)
    {
        var patient = await _patientRepository.GetAsync($"where Email = {request.Email}");

        if (patient is null || !PasswordHasher.Verify(patient.HashedPassword, request.Password))
        {
            _notification.AddNotification(ErrorMessages.User_Not_Found);
            return default;
        }

        var token = GenerateToken(patient);

        return token;
    }

    private string GenerateToken(Model.Patient patient)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, patient.Id.ToString()),
                new Claim(ClaimTypes.Name, patient.Name),
                new Claim(ClaimTypes.Email, patient.Email),
                new Claim(ClaimTypes.MobilePhone, patient.Phone),
                new Claim(ClaimTypes.Locality, patient.Localization())
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
