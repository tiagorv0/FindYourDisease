using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Repository;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Result<PatientResponse>>
{
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<PatientResponse>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.Id, cancellationToken);

        if(patient == null)
            return Result<PatientResponse>.Failure(Error.Patient_Not_Found);

        patient.BirthDate = patient.BirthDate == request.PatientRequest.BirthDate ? patient.BirthDate : request.PatientRequest.BirthDate;

        var result = await _patientRepository.GetByIdAsync(patient.Id, cancellationToken);

        return Result<PatientResponse>.Success(PatientResponse.FromPatient(result));
    }
}