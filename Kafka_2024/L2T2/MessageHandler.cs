using Streamiz.Kafka.Net.Stream;

namespace L2T2
{
    public class MessageHandler
    {
        public IKStream<string, string>[] FilterAndBranchRecords(IKStream<string, string> stream)
        {
            return stream
                .FilterNot((k, v) => v == null)
                .FlatMap((k, v) =>
                    v.Split()
                        // Word-based filter
                        .Where(word => !string.IsNullOrEmpty(word))
                        // Transform to KeyValuePair
                        .Select(word => KeyValuePair.Create(word.Length.ToString(), word))
)
                .Branch(
                    (k, v) => int.Parse(k) < 10 && v.Contains('a'),
                    (k, v) => int.Parse(k) >= 10 && v.Contains('a')
                );
        }

        public IKStream<string, string> MergeAndFilterRecords(IKStream<string, string>[] streams)
        {
            return streams[0]
                .Merge(streams[1])
                .Filter((k, v) => v.Contains("a"));
        }
    }
}