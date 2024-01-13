using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly INotificationCollector _notificationCollector;
    private readonly ICachingService _cachingService;

    public UpdatePatientCommandHandler(IPatientRepository patientRepository,
        IFileStorageService fileStorageService,
        INotificationCollector notificationCollector,
        ICachingService cachingService)
    {
        _patientRepository = patientRepository;
        _fileStorageService = fileStorageService;
        _notificationCollector = notificationCollector;
        _cachingService = cachingService;
    }

    public async Task<PatientResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await ValidationAsync(request, cancellationToken);

        if (_notificationCollector.HasNotifications())
            return default;

        if (request.PatientRequest.Photo is not null)
        {
            var updatedFileName = await _fileStorageService
                .UpdateFileAsync(patient.Photo, request.PatientRequest.Photo, Path.GetExtension(request.PatientRequest.Photo.FileName));

            patient.ChangePhoto(updatedFileName);
            if (_notificationCollector.HasNotifications())
                return default;
        }

        if (!string.IsNullOrEmpty(request.PatientRequest.Password))
            patient.ChangePassword(PasswordHasher.Hash(request.PatientRequest.Password));

        patient.Update(request.PatientRequest.Name, request.PatientRequest.Description, request.PatientRequest.Email, request.PatientRequest.Phone,
            request.PatientRequest.BirthDate, request.PatientRequest.City, request.PatientRequest.State, request.PatientRequest.Country);

        await _patientRepository.UpdateAsync(patient, cancellationToken);

        await _cachingService.SetAsync(request.SetCacheKey(patient.Id), patient);

        return PatientResponse.FromPatient(patient);
    }

    private async Task<Domain.Model.Patient?> ValidationAsync(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, null, cancellationToken);

        if (patient is null)
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);

        var patientByEmail = await _patientRepository.GetAsync("Email", request.PatientRequest.Email, cancellationToken);

        if (patientByEmail is not null && patientByEmail.Id != request.Id)
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);

        var patientByPhone = await _patientRepository.GetAsync("Phone", request.PatientRequest.Phone, cancellationToken);

        if (patientByPhone is not null && patientByPhone.Id != request.Id)
            _notificationCollector.AddNotification(ErrorMessages.Phone_Already_Exist);

        return patient;
    }
}