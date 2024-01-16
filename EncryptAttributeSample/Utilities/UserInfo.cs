using Newtonsoft.Json;
using EncryptAttributeSample.Attributes;

namespace EncryptAttributeSample.Utilities;

public class UserInfo(int id, string userName)
{
    [JsonConverter(typeof(UserEncryptor))]
    public int Id { get; set; } = id;
    public string UserName { get; set; } = userName;
}