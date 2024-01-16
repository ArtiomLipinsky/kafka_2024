using Confluent.Kafka;
using Lib;
using System.Text.Json;
using L1T2.Signal;

namespace L1T2.Consumer
{
    public class TaxiConsumer : IConsumer
    {
        private readonly IProducer _producer;
        private readonly IConsumer<string,string> _consumer;

        public TaxiConsumer()
        {
            _producer = new Producer(GlobalConfig.OutputTopicsL1T2[0]);
            _consumer = new ConsumerBuilder<string, string>(GlobalConfig.ConsumerConfig).Build();
            _consumer.Subscribe(GlobalConfig.InputTopicsL1T2);
        }

        public (string, string) Consume()
        {
            var consumeResult = _consumer.Consume();

            Console.WriteLine($"Key: {consumeResult.Key}, Value: {consumeResult.Message.Value}");

            var res = JsonSerializer.Deserialize<SignalDto>(consumeResult.Message.Value);

            var range = Coordinator.Push(res);

            _producer.Produce(range.ToString(), res.Id.ToString());

            _consumer.Commit(consumeResult);

            return (consumeResult.Message.Key, consumeResult.Message.Value);
        }
    }
}
