using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class DeletePatientCommand : CachePatient, IRequest<PatientResponse>
{
    public long Id { get; set; }
}
