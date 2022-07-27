using Kafka.Producer.DTOs.Request;

namespace Kafka.Producer.Interfaces.Producer
{
    public interface IPixProducer
    {
        Task Producer(PixProducerRequest pixProducerRequest);
    }
}
