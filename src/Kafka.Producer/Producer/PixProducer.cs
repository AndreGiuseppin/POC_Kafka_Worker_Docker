using Confluent.Kafka;
using Kafka.Producer.Configurations;
using Kafka.Producer.DTOs.Request;
using Kafka.Producer.Interfaces.Producer;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Kafka.Producer.Producer
{
    public class PixProducer : IPixProducer
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IOptions<KafkaConfiguration> _configuration;


        public PixProducer(IProducer<Null, string> producer,
            IOptions<KafkaConfiguration> configuration)
        {
            _producer = producer;
            _configuration = configuration;
        }

        public async Task Producer(PixProducerRequest pixProducerRequest)
        {
            try
            {
                await _producer.ProduceAsync(_configuration.Value.PixTopic,
                            new Message<Null, string> { Value = JsonSerializer.Serialize(pixProducerRequest) });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
