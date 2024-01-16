using Confluent.Kafka;

namespace Kafka_2024.Common.Producer
{
    public interface IProducer
    {
        Task<DeliveryResult<object, string>> Produce(string message);
    }
}
