using Streamiz.Kafka.Net.Mock;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2T5.Tests
{
    public class L2T4Test
    {
        [Fact]
        public void Test_Successful()
        {
            var config = new StreamConfig<StringSerDes, JsonPersonSerDes>()
            {
                ApplicationId = "TestL2T4",
                DefaultKeySerDes = new StringSerDes(),
                DefaultValueSerDes = new JsonPersonSerDes()
            };
            var builder = new StreamBuilder();
            var inputStream = builder.Stream<string, Person>("test");

            Topology t = builder.Build();

            using (var driver = new TopologyTestDriver(t, config))
            {
                var inputTopic = driver.CreateInputTopic<string, Person>("test");
                inputTopic.PipeInput("TestKey", new Person { Company = "EPAM", Name = "John", Position = "developer", Experience = 5 });
                inputStream.Foreach((k, v) =>
                {
                    Assert.Equal("John", v.Name);
                    Assert.Equal("EPAM", v.Company);
                    Assert.Equal("developer", v.Position);
                    Assert.Equal(5, v.Experience);
                });
            }
        }
    }
}
