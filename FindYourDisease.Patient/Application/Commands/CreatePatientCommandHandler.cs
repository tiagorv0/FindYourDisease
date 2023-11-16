using FindYourDisease.Patient.Application.Service;
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
        var fileName = await _fileStorageService.SaveFileAsync(request.PatientRequest.Photo, Path.GetExtension(request.PatientRequest.Photo.FileName));

        if (_notificationCollector.HasNotifications())
            return default;

        var patient = PatientRequest.ToPatient(request.PatientRequest);
        patient.Photo = fileName;

        patient.HashedPassword = PasswordHasher.Hash(request.PatientRequest.Password);

        await _patientRepository.CreateAsync(patient, cancellationToken);

        return PatientResponse.FromPatient(patient);
    }
}
