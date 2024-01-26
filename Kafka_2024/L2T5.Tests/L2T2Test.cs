using Streamiz.Kafka.Net.Mock;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using L2T2;

namespace L2T5.Tests
{
    public class L2T2Test
    {
        [Fact]
        public void FilterAndBranchRecords_Successful()
        {
            var config = new StreamConfig<StringSerDes, StringSerDes>()
            {
                ApplicationId = "TestL2T2"
            };

            var builder = new StreamBuilder();
            var inputStream = builder.Stream<string, string>("testStream");

            Topology t = builder.Build();

            var messageHandler = new MessageHandler();

            using (var driver = new TopologyTestDriver(t, config))
            {
                var inputTopic = driver.CreateInputTopic<string, string>("testStream");
                inputTopic.PipeInput("TestKey", "message to kafka streams with no letter a in it");
                var brachStreams = messageHandler.FilterAndBranchRecords(inputStream);
                Assert.Equal(2, brachStreams.Length);
                brachStreams[0].Foreach((k, v) =>
                {
                    Assert.True(int.Parse(k) < 10);
                });
                brachStreams[1].Foreach((k, v) =>
                {
                    Assert.True(int.Parse(k) >= 10);
                });
            }
        }

        [Fact]
        public void MergeAndFilterRecords_Successful()
        {
            var config = new StreamConfig<StringSerDes, StringSerDes>()
            {
                ApplicationId = "TestL2T2"
            };

            var builder = new StreamBuilder();
            var inputStream = builder.Stream<string, string>("testStream");

            Topology t = builder.Build();
            var messageHandler = new MessageHandler();

            using (var driver = new TopologyTestDriver(t, config))
            {
                var inputTopic = driver.CreateInputTopic<string, string>("testStream");
                inputTopic.PipeInput("TestKey", "message to kafka streams with no letter a in it");
                var brachStreams = messageHandler.FilterAndBranchRecords(inputStream);
                var resultStream = messageHandler.MergeAndFilterRecords(brachStreams);
                resultStream.Foreach((k, v) =>
                {
                    Assert.True(v.Contains('a'));
                });
            }
        }
    }
}
