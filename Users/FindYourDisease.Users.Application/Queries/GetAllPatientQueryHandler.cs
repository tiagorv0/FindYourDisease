using FindYourDisease.Users.Application.DTO;
using FindYourDisease.Users.Domain.Interfaces;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientResponse>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientResponse>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(request.Active, cancellationToken);

        return PatientResponse.FromPatient(patients);
    }
}
