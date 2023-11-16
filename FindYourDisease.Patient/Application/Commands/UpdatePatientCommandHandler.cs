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
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        string? updatedFileName = null;
        if(request.PatientRequest.Photo == null)
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

        await _patientRepository.UpdateAsync(patient, cancellationToken);

        return PatientResponse.FromPatient(patient);
    }
}