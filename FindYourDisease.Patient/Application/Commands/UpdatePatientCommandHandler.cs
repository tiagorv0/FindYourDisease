using FindYourDisease.Patient.Application.Service;
using FindYourDisease.Patient.Domain.Abstractions;
using FindYourDisease.Patient.Domain.DTO;
using FindYourDisease.Patient.Infra.Repository;
using MediatR;
using SecureIdentity.Password;

namespace FindYourDisease.Patient.Application.Commands;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly INotificationCollector _notificationCollector;

    public UpdatePatientCommandHandler(IPatientRepository patientRepository,
        IFileStorageService fileStorageService,
        INotificationCollector notificationCollector)
    {
        _patientRepository = patientRepository;
        _fileStorageService = fileStorageService;
        _notificationCollector = notificationCollector;
    }

    public async Task<PatientResponse> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await ValidationAsync(request, cancellationToken);

        if (_notificationCollector.HasNotifications())
            return default;

        string? updatedFileName = null;
        if(request.PatientRequest.Photo is not null)
        {
            updatedFileName = await _fileStorageService
                .UpdateFileAsync(patient.Photo, request.PatientRequest.Photo, Path.GetExtension(request.PatientRequest.Photo.FileName));

            if (_notificationCollector.HasNotifications())
                return default;
        }

        if (!string.IsNullOrEmpty(request.PatientRequest.Password))
            patient.HashedPassword = PasswordHasher.Hash(request.PatientRequest.Password);

        patient.Name = request.PatientRequest.Name;
        patient.Description = request.PatientRequest.Description;
        patient.Email = request.PatientRequest.Email;
        patient.Phone = request.PatientRequest.Phone;
        patient.Photo = updatedFileName ?? patient.Photo;
        patient.BirthDate = request.PatientRequest.BirthDate;
        patient.City = request.PatientRequest.City;
        patient.State = request.PatientRequest.State;
        patient.Country = request.PatientRequest.Country;
        patient.Active = request.PatientRequest.Active;

        await _patientRepository.UpdateAsync(patient, cancellationToken);

        return PatientResponse.FromPatient(patient);
    }

    private async Task<Domain.Model.Patient> ValidationAsync(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, null, cancellationToken);

        var patientByEmail = await _patientRepository.GetAsync("Email", request.PatientRequest.Email);

        if (patient is null)
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);

        if (patientByEmail is not null && patientByEmail.Id != request.Id)
            _notificationCollector.AddNotification(ErrorMessages.Email_Already_Exist);

        return patient;
    }
}