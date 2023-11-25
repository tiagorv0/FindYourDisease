using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetByIdPatientQuery : IRequest<PatientResponse>
{
    public long Id { get; set; }
}
