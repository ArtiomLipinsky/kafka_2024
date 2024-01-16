using Lib;

IProducer producer = new Producer();

for (int i = 0; i < Mock.Messages.Length; i++)
{
    var result = producer.Produce(Mock.Messages[i], $"{i}").Result;

    Console.WriteLine($"Produced '{result.Topic}':'{result.Partition}':'{result.Value}':'{result.TopicPartitionOffset}'");
}



