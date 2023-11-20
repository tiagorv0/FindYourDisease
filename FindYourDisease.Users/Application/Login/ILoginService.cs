using FindYourDisease.Users.Domain.DTO;

namespace FindYourDisease.Users.Application.Login;

public interface ILoginService
{
    Task<string> LoginAsync(LoginRequest request);
}
