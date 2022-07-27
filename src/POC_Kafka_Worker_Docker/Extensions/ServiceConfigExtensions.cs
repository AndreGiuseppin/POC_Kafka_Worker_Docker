using Confluent.Kafka;
using Kafka.Producer.Configurations;
using Kafka.Producer.Interfaces.Producer;
using Kafka.Producer.Producer;
using System.Net;

namespace POC_Kafka_Worker_Docker.Extensions
{
    public static class ServiceConfigExtensions
    {
        public static void SetupDependencyInjection(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IPixProducer, PixProducer>();
        }

        public static void AddKafka(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<KafkaConfiguration>().BindConfiguration("Kafka");
            var kafka = new KafkaConfiguration();
            configuration.GetSection("Kafka").Bind(kafka);

            var configConsumer = new ConsumerConfig
            {
                GroupId = kafka.GroupId,
                BootstrapServers = kafka.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                MaxPollIntervalMs = 600000,
                SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), kafka.SecurityProtocol)
            };

            var configProducer = new ProducerConfig
            {
                BootstrapServers = kafka.BootstrapServers,
                ClientId = Dns.GetHostName(),
                SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), kafka.SecurityProtocol),
                Partitioner = Partitioner.ConsistentRandom
            };

            services.AddTransient(_ => new ConsumerBuilder<Ignore, string>(configConsumer).Build());
            services.AddTransient(_ => new ProducerBuilder<Null, string>(configProducer).Build());
        }
    }
}
