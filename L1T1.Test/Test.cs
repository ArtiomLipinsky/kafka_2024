using Lib;

namespace L1T1.Test
{
    public class Test
    {
        [Fact]
        async public void L1T1Test()
        {
            IProducer producer = new Producer();
            IConsumer consumer = new Consumer();

            var counter = 0;

            // Act
            foreach (var message in Mock.Messages)
            {
                counter++;
                await producer.Produce(message, $"{counter}");
            }

            var consumedMessages = new List<string>();

            for (int i = 0; i < counter; i++)
            {
                var message = consumer.Consume();
                consumedMessages.Add(message.Item2);
            }

            // Assert
            foreach (var consumedMessage in consumedMessages)
            {
                Assert.Contains(consumedMessage, Mock.Messages);
            }

            Assert.Equal(consumedMessages.Count, Mock.Messages.Count());
        }
    }
}