using FindYourDisease.Patient.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Queries;

public class GetAllPatientQuery : IRequest<IEnumerable<PatientResponse>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string OrderBy { get; set; }
    public string Search { get; set; }
}
