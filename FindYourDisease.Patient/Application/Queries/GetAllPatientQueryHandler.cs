using FindYourDisease.Patient.Domain.DTO;
using FindYourDisease.Patient.Infra.Repository;
using MediatR;

namespace FindYourDisease.Patient.Application.Queries;

public class GetAllPatientQueryHandler : IRequestHandler<GetAllPatientQuery, IEnumerable<PatientResponse>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllPatientQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<PatientResponse>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(cancellationToken);

        return PatientResponse.FromPatient(patients);
    }
}
