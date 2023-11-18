using FindYourDisease.Patient.Application.Service;
using FindYourDisease.Patient.Domain.Abstractions;
using FindYourDisease.Patient.Domain.DTO;
using FindYourDisease.Patient.Infra.Repository;
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
        var patient = await _patientRepository.GetByIdAsync(request.Id, true, cancellationToken);

        if (patient == null)
        {
            _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
            return default;
        }

        return PatientResponse.FromPatient(patient);
    }
}
