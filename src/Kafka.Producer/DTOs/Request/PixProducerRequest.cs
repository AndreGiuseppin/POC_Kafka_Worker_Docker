namespace Kafka.Producer.DTOs.Request
{
    public record PixProducerRequest(string BranchFrom, string AccountFrom, string BranchTo, string AccountTo, string Value);
}
