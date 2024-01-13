using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly ICachingService _cachingService;

    public DeletePatientCommandHandler(IPatientRepository patientRepository, INotificationCollector notificationCollector, ICachingService cachingService)
    {
        _patientRepository = patientRepository;
        _notificationCollector = notificationCollector;
        _cachingService = cachingService;
    }

    public async Task<PatientResponse> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, true, cancellationToken);

        if (patient == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        await _patientRepository.DeleteAsync(request.Id, cancellationToken);

        await _cachingService.RemoveAsync(request.SetCacheKey(patient.Id));

        return PatientResponse.FromPatient(patient);
    }
}
