using Confluent.Kafka;

namespace Lib
{
    public interface IProducer
    {
        Task<DeliveryResult<string, string>> Produce(string message, string key);
    }
}
