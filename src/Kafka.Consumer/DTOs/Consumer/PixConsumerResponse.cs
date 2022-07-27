namespace Kafka.Consumer.DTOs.Consumer
{
    public record PixConsumerResponse(string BranchFrom, string AccountFrom, string BranchTo, string AccountTo, string Value);

}
