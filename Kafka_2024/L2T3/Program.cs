using Confluent.Kafka;
using Lib;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.Stream;

var config = new StreamConfig<StringSerDes, StringSerDes>
{
    ApplicationId = "L2T3",
    BootstrapServers = GlobalConfig.ConsumerConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var builder = new StreamBuilder();

// Read from both topics
var inputTopic1 = builder.Stream<string, string>(GlobalConfig.InputTopicL2T3);
var inputTopic2 = builder.Stream<string, string>(GlobalConfig.InputTopicL2T3_1);

// Filter out null messages and they contain ':'
var validLines1 = inputTopic1.Filter((k, v) => v != null && v.Contains(":"));
var validLines2 = inputTopic2.Filter((k, v) => v != null && v.Contains(":"));

// Extract new key from the value
var keyedStream1 = validLines1.SelectKey((k, v) => v.Split(':')[0]);
var keyedStream2 = validLines2.SelectKey((k, v) => v.Split(':')[0]);

// Print every message
keyedStream1.Peek((k, v) => Console.WriteLine($"Key: {k}, Value: {v}"));
keyedStream2.Peek((k, v) => Console.WriteLine($"Key: {k}, Value: {v}"));

// Merge the streams
var resultStream = keyedStream1.Join(
    keyedStream2,
    (v1, v2) => $"{v1}-{v2}",
    JoinWindowOptions.Of(TimeSpan.FromMinutes(1)));

// Join results output
resultStream.Foreach((k, v) => Console.WriteLine($" Merged Key:{k}, Value:{v}"));

var topology = builder.Build();


var stream = new KafkaStream(topology, config);

await stream.StartAsync();