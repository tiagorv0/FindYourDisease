using FindYourDisease.Patient.Domain.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class CreatePatientCommand : IRequest<PatientResponse>
{
    public PatientRequest PatientRequest { get; set; }
}
