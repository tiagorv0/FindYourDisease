using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class UpdatePatientCommand : IRequest<PatientResponse>
{
    public long Id { get; set; }
    public PatientRequest PatientRequest { get; set; }
}
