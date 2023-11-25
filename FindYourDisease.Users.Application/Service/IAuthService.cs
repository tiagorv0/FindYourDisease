using FindYourDisease.Users.Domain.Model;

namespace FindYourDisease.Users.Application.Service;
public interface IAuthService
{
    string GenerateTokenUser(User user);
    string GenerateTokenPatient(Patient patient);
}
