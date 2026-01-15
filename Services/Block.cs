using mini_blockchain.Crypto;
using System.Security.Cryptography;


namespace mini_blockchain.Services;

class Block
{
    public string Document { get; set; } = string.Empty;
    public string DocumentHash { get; set; } = string.Empty;
    public byte[] DigitalSignature { get; set; } = Array.Empty<byte>();
    public string PreviousBlockHash { get; set; } = string.Empty;
    public string BlockHash { get; set; } = string.Empty;

    public static Block CreateSignedBlock(string document, string previousHash, RSA rsa)
    {
        string docHash = CryptoUtils.ComputeHash(document);
        byte[] signature = CryptoUtils.SignData(docHash, rsa);

        Block block = new Block
        {
            Document = document,
            DocumentHash = docHash,
            DigitalSignature = signature,
            PreviousBlockHash = previousHash
        };

        block.BlockHash = block.CalculateBlockHash();
        return block;
    }

    public string CalculateBlockHash()
    {
        string data = DocumentHash + PreviousBlockHash;
        return CryptoUtils.ComputeHash(data);
    }
}