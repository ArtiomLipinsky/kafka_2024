using Mock;
using Kafka_2024.Common.Producer;


namespace ProducerProg
{
    static class Start
    {
        static void Main()
        {
            IProducer producer = new Producer();

            foreach (var message in Mock.Messages)
            {
                var result = producer.Produce(message).Result;

                Console.WriteLine($"Produced '{result.Topic}':'{result.Partition}':'{result.Value}':'{result.TopicPartitionOffset}'");
            }
        }
    }
}



