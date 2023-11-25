using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class DeletePatientCommand : IRequest<PatientResponse>
{
    public long Id { get; set; }
}
