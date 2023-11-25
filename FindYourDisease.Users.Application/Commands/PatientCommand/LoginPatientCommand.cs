using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class LoginPatientCommand : IRequest<string>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
