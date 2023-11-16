using FindYourDisease.Patient.Domain.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class UpdatePatientCommand : IRequest<PatientResponse>
{
    public long Id { get; set; }
    public PatientRequest PatientRequest { get; set; }
}
