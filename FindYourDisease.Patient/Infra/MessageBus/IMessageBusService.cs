namespace FindYourDisease.Patient.Infra.MessageBus;

public interface IMessageBusService
{
    void Publish<T>(string queue, T message) where T : class;
}
