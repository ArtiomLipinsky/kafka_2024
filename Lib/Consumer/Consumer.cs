using Confluent.Kafka;

namespace Lib
{
    public class Consumer : IConsumer
    {
        private readonly string[] _inputs;
        private readonly IConsumer<string,string> _consumer;

        public Consumer()
        {
            _consumer = new ConsumerBuilder<string, string>(GlobalConfig.ConsumerConfig).Build();
            _consumer.Subscribe(GlobalConfig.InputTopicsL1T1);
        }

        public Consumer(string[] customeinputs)
        {
            _consumer = new ConsumerBuilder<string, string>(GlobalConfig.ConsumerConfig).Build();
            _consumer.Subscribe(customeinputs);
        }

        public (string, string) Consume()
        {
            var msg = _consumer.Consume();
            _consumer.Commit(msg);
            return (msg.Message.Key, msg.Message.Value);
        }
    }
}
