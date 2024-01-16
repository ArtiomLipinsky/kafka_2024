using Confluent.Kafka;
using Kafka_2024.Common.Config;

namespace Kafka_2024.Common.Consumer
{
    public class Consumer : IConsumer
    {
        public string Consume()
        {
            using var consumer = new ConsumerBuilder<Ignore, string>(GlobalConfig.ConsumerConfig).Build();

            try
            {
                consumer.Subscribe(GlobalConfig.InputTopics);
                return consumer.Consume().Message.Value;
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
