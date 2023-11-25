using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;
public class LoginPatientCommandHandler : IRequestHandler<LoginPatientCommand, string>
{
    private readonly IAuthService _authService;
    private readonly INotificationCollector _notification;
    private readonly IPatientRepository _patientRepository;

    public LoginPatientCommandHandler(IAuthService authService, INotificationCollector notification, IPatientRepository patientRepository)
    {
        _authService = authService;
        _notification = notification;
        _patientRepository = patientRepository;
    }

    public async Task<string> Handle(LoginPatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetAsync("Email", request.Email);

        if (patient is null || !PasswordHasher.Verify(patient.HashedPassword, request.Password))
        {
            _notification.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        var token = _authService.GenerateTokenPatient(patient);

        return token;
    }
}
