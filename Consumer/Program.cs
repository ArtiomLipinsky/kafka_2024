using Lib;

IConsumer consumer = new Consumer();

while (true)
{
    Console.WriteLine($"Consumed: {consumer.Consume()}");
}
