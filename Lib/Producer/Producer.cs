using Confluent.Kafka;

namespace Lib
{
    public class Producer : IProducer
    {
        private IProducer<string, string> producer;
        private string topic;

        public Producer()
        {
            producer = new ProducerBuilder<string, string>(GlobalConfig.ProducerConfig).Build();
            topic = GlobalConfig.InputTopicsL1T1[0];
        }

        public Producer(string topicToProduce)
        {
            producer = new ProducerBuilder<string, string>(GlobalConfig.ProducerConfig).Build();
            topic = topicToProduce;
        }

        public async Task<DeliveryResult<string, string>> Produce(string message, string key) 
            => await producer.ProduceAsync(topic ?? GlobalConfig.InputTopicsL1T1.FirstOrDefault(), new Message<string, string> { Value = message, Key = key });
    }
}
