using FindYourDisease.Patient.Abstractions;
using FindYourDisease.Patient.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class CreatePatientCommand : IRequest<Result<PatientResponse>>
{
    public PatientRequest PatientRequest { get; set; }
}
