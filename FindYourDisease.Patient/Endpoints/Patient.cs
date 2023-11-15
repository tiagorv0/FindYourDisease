using FindYourDisease.Patient.Application.Commands;
using FindYourDisease.Patient.DTO;
using FindYourDisease.Patient.Service;
using MediatR;

namespace FindYourDisease.Patient.Endpoints;

public static class Patient
{
    public static WebApplication MapPostCreatePatient(this WebApplication app)
    {
        app.MapPost("patient/create", async (PatientRequest patient, IMediator _mediator, INotificationCollector _notification) =>
        {
            var command = new CreatePatientCommand
            {
                PatientRequest = patient
            };

            var response = await _mediator.Send(command);

            return !_notification.HasNotifications() ? Results.Ok(response) : Results.BadRequest(_notification.GetNotifications());
        });


        return app;
    }
}
