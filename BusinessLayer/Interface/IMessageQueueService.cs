namespace BusinessLayer.Interface
{
    public interface IMessageQueueService
    {
        Task PublishMessage(string queueName, string message);
    }
}
