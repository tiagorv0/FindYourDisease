using FindYourDisease.Patient.Application.Service;
using FindYourDisease.Patient.Domain.Abstractions;
using FindYourDisease.Patient.Domain.DTO;
using FindYourDisease.Patient.Infra.Repository;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Patient.Application.Commands;

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

        string fileName = null;
        if(request.PatientRequest.Photo is not null)
        {
            fileName = await _fileStorageService.SaveFileAsync(request.PatientRequest.Photo, Path.GetExtension(request.PatientRequest.Photo.FileName));
            if (_notificationCollector.HasNotifications())
                return default;
        }

        var patient = PatientRequest.ToPatient(request.PatientRequest);
        patient.Photo = fileName;

        patient.HashedPassword = PasswordHasher.Hash(request.PatientRequest.Password);

        var patientId = await _patientRepository.CreateAsync(patient, cancellationToken);
        patient.Id = patientId;

        return PatientResponse.FromPatient(patient);
    }
}
