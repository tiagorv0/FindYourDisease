using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Commands.PatientCommand;

public class CreatePatientCommand : CachePatient, IRequest<PatientResponse>
{
    public PatientRequest PatientRequest { get; set; }
}
