using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Application.Service;
using FindYourDisease.Users.Domain.Abstractions;
using FindYourDisease.Users.Domain.Interfaces;
using FindYourDisease.Users.Domain.Model;
using MediatR;
using System.Text.Json;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdPatientQueryHandler : IRequestHandler<GetByIdPatientQuery, PatientResponse>
{
    private readonly IPatientRepository _patientRepository;
    private readonly INotificationCollector _notificationCollector;
    private readonly ICachingService _cachingService;

    public GetByIdPatientQueryHandler(IPatientRepository patientRepository, 
        INotificationCollector notificationCollector, 
        ICachingService cachingService)
    {
        _patientRepository = patientRepository;
        _notificationCollector = notificationCollector;
        _cachingService = cachingService;
    }

    public async Task<PatientResponse> Handle(GetByIdPatientQuery request, CancellationToken cancellationToken)
    {
        var cache = await _cachingService.GetAsync(request.SetCacheKey(request.Id));

        if(cache is null)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id, true, cancellationToken);

            if (patient is null)
            {
                _notificationCollector.AddNotification(ErrorMessages.Patient_Not_Found);
                return default;
            }

            await _cachingService.SetAsync(request.SetCacheKey(patient.Id), patient);
            return PatientResponse.FromPatient(patient);
        }
        
        var cachedPatient = JsonSerializer.Deserialize<Patient>(cache);
        return PatientResponse.FromPatient(cachedPatient);
    }
}
