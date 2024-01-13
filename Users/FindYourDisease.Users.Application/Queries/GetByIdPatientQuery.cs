using FindYourDisease.Users.Application.Caching;
using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdPatientQuery : CachePatient, IRequest<PatientResponse>
{
    public long Id { get; set; }
}
