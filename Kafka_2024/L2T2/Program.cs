using Confluent.Kafka;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net;
using Lib;
using L2T2;

var config = new StreamConfig<StringSerDes, StringSerDes>
{
    ApplicationId = "L2T2",
    BootstrapServers = GlobalConfig.ConsumerConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

var builder = new StreamBuilder();

var textLines = builder.Stream<string, string>(GlobalConfig.InputTopicL2T2);

var handler = new MessageHandler();

// Split sentences into words and transform into new messages with length as key
var splitLines = handler.FilterAndBranchRecords(textLines);

// Merge the streams
var mergedStream = handler.MergeAndFilterRecords(splitLines);

mergedStream.Foreach((k, v) => Console.WriteLine($"Length: {k}, Word: {v}"));

var topology = builder.Build();

// Start the Kafka Stream
var stream = new KafkaStream(topology, config);
await stream.StartAsync();