using Newtonsoft.Json;
using EncryptAttributeSample.Utilities;

namespace EncryptAttributeSample.Attributes;

public class JsonIntEncryptor(string key) : JsonConverter<int>
{
    public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        var encrypted = reader.Value!.ToString();
        var decrypted = encrypted!.Decrypt(key);
        int.TryParse(decrypted, out var integerValue);
        return integerValue;
    }

    public override void WriteJson(JsonWriter writer, int value, Newtonsoft.Json.JsonSerializer serializer)
    {
        var stringValue = value.ToString();
        var encrypted = stringValue.Encrypt(key);
        writer.WriteValue(encrypted);
    }
}
public class JsonNullableIntEncryptor(string key) : JsonConverter<int?>
{
    public string Key { get; } = key;

    public override int? ReadJson(JsonReader reader, Type objectType, int? existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        var encrypted = reader.Value!.ToString();
        var decrypted = encrypted!.Decrypt(Key);
        if (string.IsNullOrEmpty(encrypted))
            return null;
        int.TryParse(decrypted, out var integerValue);
        return integerValue!;
    }

    public override void WriteJson(JsonWriter writer, int? value, Newtonsoft.Json.JsonSerializer serializer)
    {
        var stringValue = "";
        if (value != null)
            stringValue = value.ToString();
        var encrypted = stringValue!.Encrypt(Key);
        writer.WriteValue(encrypted);
    }
}