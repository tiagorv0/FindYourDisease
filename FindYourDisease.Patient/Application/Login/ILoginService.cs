using FindYourDisease.Patient.Domain.DTO;

namespace FindYourDisease.Patient.Application.Login;

public interface ILoginService
{
    Task<string> LoginAsync(LoginRequest request);
}
