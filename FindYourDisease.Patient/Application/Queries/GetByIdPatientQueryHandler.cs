using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Repository;
using FindYourDisease.Patient.Service;
using MediatR;

namespace FindYourDisease.Patient.Application.Queries;

public class GetByIdPatientQueryHandler : IRequestHandler<GetByIdPatientQuery, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INotificationCollector _notificationCollector;

    public GetByIdPatientQueryHandler(IPatientRepository patientRepository, INotificationCollector notificationCollector)
    {
        _patientRepository = patientRepository;
        _notificationCollector = notificationCollector;
    }

    public async Task<PatientResponse> Handle(GetByIdPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (patient == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        return PatientResponse.FromPatient(patient);
    }
}
