using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Repository;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Result<PatientResponse>>
{
    private readonly IPatientRepository _patientRepository;

    public CreatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientResponse>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = PatientRequest.ToPatient(request.PatientRequest);

        var result = await _patientRepository.GetByIdAsync(patient.Id, cancellationToken);

        return Result<PatientResponse>.Success(PatientResponse.FromPatient(result));
    }
}
