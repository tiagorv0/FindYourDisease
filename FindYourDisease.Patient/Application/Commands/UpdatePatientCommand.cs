using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class UpdatePatientCommand : IRequest<Result<PatientResponse>>
{
    public long Id { get; set; }
    public PatientRequest PatientRequest { get; set; }
}
