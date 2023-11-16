using FindYourDisease.Patient.Domain.DTO;
using MediatR;

namespace FindYourDisease.Patient.Application.Commands;

public class DeletePatientCommand : IRequest<PatientResponse>
{
    public long Id { get; set; }
}
