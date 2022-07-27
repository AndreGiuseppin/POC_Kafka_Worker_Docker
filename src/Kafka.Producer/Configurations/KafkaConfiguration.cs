namespace Kafka.Producer.Configurations
{
    public class KafkaConfiguration
    {
        public string GroupId { get; set; }
        public string BootstrapServers { get; set; }
        public string SecurityProtocol { get; set; }
        public string PixTopic { get; set; }
    }
}
