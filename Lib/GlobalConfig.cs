using Confluent.Kafka;

namespace Lib
{
    public static class GlobalConfig
    {
        public static ProducerConfig ProducerConfig = new()
        {
            BootstrapServers = "localhost:8097",
            EnableDeliveryReports = true,
            Acks = Acks.Leader
        };

        public static ConsumerConfig ConsumerConfig = new()
        {
            BootstrapServers = "localhost:8097",
            GroupId = "group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            AutoCommitIntervalMs = 100,
        };

        public static string[] InputTopicsL1T1 = { "L1T1-input" };
        public static string[] InputTopicsL1T2 = { "L1T2-input" };
        public static string[] OutputTopicsL1T2 = { "L1T2-output" };
    }
}
