using System.Security.Cryptography;
using System.Text;

namespace mini_blockchain.Crypto;

public static class CryptoUtils
{
    public static string ComputeHash(string data)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToHexString(bytes);
    }

    public static byte[] SignData(string dataHash, RSA rsa)
    {
        return rsa.SignData(
            Encoding.UTF8.GetBytes(dataHash),
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );
    }

    public static bool VerifySignature(string dataHash, byte[] signature, RSA rsa)
    {
        return rsa.VerifyData(
            Encoding.UTF8.GetBytes(dataHash),
            signature,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1
        );
    }
}
