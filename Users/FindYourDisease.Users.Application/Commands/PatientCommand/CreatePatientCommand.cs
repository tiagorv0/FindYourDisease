using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class CreatePatientCommand : IRequest<PatientResponse>
{
    public PatientRequest PatientRequest { get; set; }
}
