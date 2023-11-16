using FindYourDisease.Patient.Domain.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Queries;

public class GetByIdPatientQuery : IRequest<PatientResponse>
{
    public long Id { get; set; }
}
