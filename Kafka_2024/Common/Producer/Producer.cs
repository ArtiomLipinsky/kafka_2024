using Confluent.Kafka;
using Kafka_2024.Common.Config;

namespace Kafka_2024.Common.Producer
{
    public class Producer : IProducer
    {
        private IProducer<object, string> producer;

        public Producer()
        {
            producer = new ProducerBuilder<object, string>(GlobalConfig.ProducerConfig).Build();
        }

        public async Task<DeliveryResult<object, string>> Produce(string message) 
            => await producer.ProduceAsync(GlobalConfig.InputTopics.FirstOrDefault(), new Message<object, string> { Value = message });
    }
}
