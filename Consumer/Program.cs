using Lib;

// docker-compose -f docker-compose-l1.yml up

IConsumer consumer = new Consumer();

while (true)
{
    Console.WriteLine($"Consumed: {consumer.Consume()}");
}
