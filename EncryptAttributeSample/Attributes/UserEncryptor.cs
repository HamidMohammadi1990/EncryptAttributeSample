using EncryptAttributeSample.Models;

namespace EncryptAttributeSample.Attributes;

public class UserEncryptor() : JsonIntEncryptor(SecurityKeyConstant.User) { }