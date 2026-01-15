using mini_blockchain.Services;
using System.Security.Cryptography;

Console.WriteLine(" MINI BLOCKCHAIN CON FIRMA DIGITAL ===\n");

// 1. Generate RSA keys
using RSA rsa = RSA.Create(2048);

// 2. Initialize blockchain
Blockchain blockchain = new Blockchain();

// 3. Original document to be signed
string document = "Ejemplo de certificado: Marcelo Rosales - Master en Seguridad de la Informacion";

Console.WriteLine($"Documento original: {document} \n");

// 4. Create and add signed block
Block block = Block.CreateSignedBlock(
    document,
    blockchain.GetLastBlockHash(),
    rsa
);

// 5. Add block to blockchain
blockchain.AddBlock(block);


Console.WriteLine("Bloque agregado correctamente.");
Console.WriteLine($"Hash del bloque: {block.BlockHash}");


// 6. Verify blockchain integrity
Console.WriteLine("\nVerificando blockchain...");
Console.WriteLine(blockchain.VerifyChain(rsa)
    ? "Blockchain VALIDA"
    : "Blockchain INVALIDA");


Console.ReadKey();

// 6. Modify the document to simulate tampering
Console.WriteLine("\n --- MODIFICANDO DOCUMENTO --- \n");
block.Document = "Ejemplo de certificado: Jonathan Rocano - Master en Seguridad de la Informacion";

Console.WriteLine($"Documento modificado: { block.Document } \n");

Console.WriteLine("Verificando blockchain después del cambio...");
Console.WriteLine(blockchain.VerifyChain(rsa)
    ? "Blockchain VALIDA"
    : "Blockchain INVALIDA");