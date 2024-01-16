using Kafka_2024.Common.Consumer;

IConsumer consumer = new Consumer();

while (true)
{
    Console.WriteLine($"Consumed: {consumer.Consume()}");
}


