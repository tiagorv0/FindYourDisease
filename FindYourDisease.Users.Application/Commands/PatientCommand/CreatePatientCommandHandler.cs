using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly INotificationCollector _notificationCollector;

    public CreatePatientCommandHandler(IPatientRepository patientRepository, IFileStorageService fileStorageService, INotificationCollector notificationCollector)
    {
        _patientRepository = patientRepository;
        _fileStorageService = fileStorageService;
        _notificationCollector = notificationCollector;
    }

    public async Task<PatientResponse> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var exist = await _patientRepository.ExistAsync("Email", request.PatientRequest.Email, cancellationToken);
        if (exist)
        {
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);
            return default;
        }

        var patient = PatientRequest.ToPatient(request.PatientRequest);
        patient.SetPassword(PasswordHasher.Hash(request.PatientRequest.Password));

        if (request.PatientRequest.Photo is not null)
        {
            var fileName = await _fileStorageService.SaveFileAsync(request.PatientRequest.Photo, Path.GetExtension(request.PatientRequest.Photo.FileName));
            patient.SetPhoto(fileName);
            if (_notificationCollector.HasNotifications())
                return default;
        }

        var patientId = await _patientRepository.CreateAsync(patient, cancellationToken);
        patient.Id = patientId;

        return PatientResponse.FromPatient(patient);
    }
}
