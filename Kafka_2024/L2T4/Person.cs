using Confluent.Kafka;
using Newtonsoft.Json;
using Streamiz.Kafka.Net.SerDes;
using System.Text;

public class Person
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("company")]
    public string Company { get; set; }

    [JsonProperty("position")]
    public string Position { get; set; }

    [JsonProperty("experience")]
    public int Experience { get; set; }
}

public class JsonPersonSerializer : ISerializer<Person>
{
    public byte[] Serialize(Person data, SerializationContext context)
    {
        return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
    }
}

public class JsonPersonDeserializer : IDeserializer<Person>
{
    public Person Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonConvert.DeserializeObject<Person>(Encoding.UTF8.GetString(data.ToArray()));
    }
}

public class JsonPersonSerDes : ISerDes<Person>
{
    private readonly JsonPersonSerializer serializer = new JsonPersonSerializer();
    private readonly JsonPersonDeserializer deserializer = new JsonPersonDeserializer();

    public Person Deserialize(string topic, byte[] data)
    {
        return deserializer.Deserialize(data, false, new SerializationContext());
    }

    public byte[] Serialize(string topic, Person data)
    {
        return serializer.Serialize(data, new SerializationContext());
    }

    Person ISerDes<Person>.Deserialize(byte[] data, SerializationContext context)
    {
        return deserializer.Deserialize(data, false, new SerializationContext());
    }

    object ISerDes.DeserializeObject(byte[] data, SerializationContext context)
    {
        return deserializer.Deserialize(data, false, new SerializationContext());
    }

    void ISerDes.Initialize(SerDesContext context)
    {
        
    }

    byte[] ISerDes<Person>.Serialize(Person data, SerializationContext context)
    {
        return serializer.Serialize(data, new SerializationContext());
    }

    byte[] ISerDes.SerializeObject(object data, SerializationContext context)
    {
        return serializer.Serialize(data as Person, new SerializationContext());
    }
}