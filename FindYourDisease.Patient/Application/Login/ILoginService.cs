using FindYourDisease.Patient.DTO;

namespace FindYourDisease.Patient.Application.Login;

public interface ILoginService
{
    Task<string> LoginAsync(LoginRequest request);
}
