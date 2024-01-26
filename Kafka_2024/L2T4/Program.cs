// Stream configuration
using Confluent.Kafka;
using Lib;
using Newtonsoft.Json;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;

var config = new StreamConfig<StringSerDes, JsonPersonSerDes>
{
    ApplicationId = "L2T4",
    BootstrapServers = GlobalConfig.ConsumerConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var builder = new StreamBuilder();

// Read from the topic
var inputTopic = builder.Stream<string, Person>(GlobalConfig.InputTopicL2T4);

// Filter out null messages and print to console
inputTopic.FilterNot((k, v) => v == null).Foreach((k, v) => Console.WriteLine($"Key: {k}, Person: {JsonConvert.SerializeObject(v)}"));

var topology = builder.Build();

var stream = new KafkaStream(topology, config);
await stream.StartAsync();