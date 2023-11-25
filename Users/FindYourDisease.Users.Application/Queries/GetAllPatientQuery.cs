using FindYourDisease.Users.Application.DTO;
using MediatR;

namespace FindYourDisease.Users.Application.Queries;

public class GetAllPatientQuery : IRequest<IEnumerable<PatientResponse>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string OrderBy { get; set; }
    public bool Active { get; set; }
    public string Search { get; set; }

}
