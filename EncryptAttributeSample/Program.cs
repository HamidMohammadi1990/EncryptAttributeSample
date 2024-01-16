using Newtonsoft.Json;
using EncryptAttributeSample.Utilities;

var userInfo = new UserInfo(int.MaxValue, "Hamid");
var serializedUserInfo = JsonConvert.SerializeObject(userInfo);
Console.WriteLine(serializedUserInfo);

var tempUserInfo = JsonConvert.DeserializeObject<UserInfo>(serializedUserInfo);

Console.ReadLine();