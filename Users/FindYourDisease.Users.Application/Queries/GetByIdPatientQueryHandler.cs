using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

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
