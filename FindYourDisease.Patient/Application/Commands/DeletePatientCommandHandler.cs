using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Repository;
using FindYourDisease.Patient.Service;
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
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        await _patientRepository.DeleteAsync(request.Id, cancellationToken);

        return PatientResponse.FromPatient(patient);
    }
}
