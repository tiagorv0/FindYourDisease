using FindYourDisease.Patient.Application.Service;
using FindYourDisease.Patient.Domain.Abstractions;
using FindYourDisease.Patient.Domain.DTO;
using FindYourDisease.Patient.Infra.Repository;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INotificationCollector _notificationCollector;

    public DeletePatientCommandHandler(IPatientRepository patientRepository, INotificationCollector notificationCollector)
    {
        _patientRepository = patientRepository;
        _notificationCollector = notificationCollector;
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

        return PatientResponse.FromPatient(patient);
    }
}
