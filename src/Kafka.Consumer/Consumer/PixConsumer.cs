using Confluent.Kafka;
using Kafka.Consumer.DTOs.Consumer;
using Kafka.Producer.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Kafka.Consumer.Consumer
{
    public class PixConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IOptions<KafkaConfiguration> _options;

        public PixConsumer(IConsumer<Ignore, string> consumer,
            IOptions<KafkaConfiguration> options)
        {
            _consumer = consumer;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            KafkaConfiguration options = _options.Value;

            try
            {
                _consumer.Subscribe(options.PixTopic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result is null || result.IsPartitionEOF || stoppingToken.IsCancellationRequested)
                        continue;

                    var message = result.Message.Value;

                    var response = JsonConvert.DeserializeObject<PixConsumerResponse>(message);

                    _consumer.Commit(result);
                    _consumer.StoreOffset(result);
                }
            }
            catch (Exception)
            {
                _consumer.Close();
                _consumer.Dispose();
            }
        }
    }
}
