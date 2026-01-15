

using mini_blockchain.Crypto;
using System.Security.Cryptography;

namespace mini_blockchain.Services;

class Blockchain
{
    private readonly List<Block> _chain = [] ;

    public Blockchain()
    {
        // Add initial block
        _chain.Add(new Block
        {
            Document = "Initial Block",
            DocumentHash = "0",
            PreviousBlockHash = "0",
            BlockHash = "0",
            DigitalSignature = Array.Empty<byte>()
        });
    }

    //Add new block to the chain
    public void AddBlock(Block block)
    {
        _chain.Add(block);
    }
    /* 
     * 
     * Get the last block's hash: being used to link new blocks
     */
    public string GetLastBlockHash()
    {
        return _chain[^1].BlockHash;
    }

    // Verify the integrity of the blockchain
    public bool VerifyChain(RSA rsa)
    {
        Console.WriteLine("\n=== INICIANDO VERIFICACION DE LA BLOCKCHAIN ===");

        for (int i = 1; i < _chain.Count; i++)
        {
            Block current = _chain[i];
            Block previous = _chain[i - 1];

            Console.WriteLine($"\n--- Verificando Bloque #{i} ---");

            // 1. Verify document hash
            string recalculatedDocHash = CryptoUtils.ComputeHash(current.Document);

            Console.WriteLine("Hash almacenado del documento:");
            Console.WriteLine(current.DocumentHash);

            Console.WriteLine("Hash recalculado del documento:");
            Console.WriteLine(recalculatedDocHash);

            if (recalculatedDocHash != current.DocumentHash)
            {
                Console.WriteLine("ERROR: El hash del documento no coincide");
                return false;
            }
            Console.WriteLine("Hash del documento valido");

            // 2. Verify digital signature
            Console.WriteLine("Verificando firma digital...");

            bool signatureValid = CryptoUtils.VerifySignature(
                current.DocumentHash,
                current.DigitalSignature,
                rsa
            );

            Console.WriteLine("Resultado de la verificacion de la firma: " +
                              (signatureValid ? "VALIDA" : "INVALIDA"));

            if (!signatureValid)
            {
                Console.WriteLine("ERROR: Firma digital invalida");
                return false;
            }

            // 3. Verify previous block hash
            Console.WriteLine("Hash del bloque anterior esperado:");
            Console.WriteLine(previous.BlockHash);

            Console.WriteLine("Hash del bloque anterior almacenado:");
            Console.WriteLine(current.PreviousBlockHash);

            if (current.PreviousBlockHash != previous.BlockHash)
            {
                Console.WriteLine("ERROR: El enlace con el bloque anterior es invalido");
                return false;
            }
            Console.WriteLine("Enlace con bloque anterior correcto");

            // 4. Verify block hash
            string recalculatedBlockHash = current.CalculateBlockHash();

            Console.WriteLine("Hash almacenado del bloque:");
            Console.WriteLine(current.BlockHash);

            Console.WriteLine("Hash recalculado del bloque:");
            Console.WriteLine(recalculatedBlockHash);

            if (recalculatedBlockHash != current.BlockHash)
            {
                Console.WriteLine("ERROR: El hash del bloque no coincide");
                return false;
            }
            Console.WriteLine("Hash del bloque valido");
        }

        Console.WriteLine("\n=== BLOCKCHAIN COMPLETAMENTE VALIDA ===");
        return true;
    }

}
