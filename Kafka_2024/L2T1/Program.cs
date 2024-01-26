using Confluent.Kafka;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Lib;


// docker-compose -f docker-compose-l2.yml up

var stringSerdes = new StringSerDes();

var config = new StreamConfig<StringSerDes, StringSerDes>
{
    BootstrapServers = GlobalConfig.ConsumerConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    ApplicationId = "L2T1",
    DefaultKeySerDes = stringSerdes,
    DefaultValueSerDes = stringSerdes,
    EnableDeliveryReports = true,
    Acks = Acks.Leader
};

var builder = new StreamBuilder();

builder.Stream<string, string>(GlobalConfig.InputTopicL2T1).To(GlobalConfig.OutputTopicL2T1);

builder.Stream<string, string>(GlobalConfig.OutputTopicL2T1).Foreach((x, y) => Console.WriteLine($"{x} {y}"));

var instance = builder.Build();

var stream = new KafkaStream(instance, config);

await stream.StartAsync();
