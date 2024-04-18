using System;
using System.IO;
using System.Security.Cryptography;
class ManagedAesSample
{
    public static void Main()
    {
        Console.WriteLine("Skriv in text du vill kryptera..");
        string data = Console.ReadLine();
        EncryptAesManaged(data);
        Console.ReadLine();
    }
    static void EncryptAesManaged(string raw)
    {
        try
        {
            //Skapa AES som genererar en ny nyckel och en ny intialiseringsvektor IV
            // Nyckeln används både i kryptering och dekryptering
            using (AesManaged aes = new AesManaged())
            {
                // Kryptera en sträng
                byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
                // Skriv ut krypterad sträng
                Console.WriteLine( "Krypterat meddelande:"+ {System.Text.Encoding.UTF8.GetString(encrypted));
                //dekryptera bitarna till sträng
                string decrypted = Decrypt(encrypted, aes.Key, aes.IV);
                // skriv ut dekrypterad sträng
                Console.WriteLine( "Dekrypterat meddelande:"+ decrypted);
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
        }
        Console.ReadKey();
    }
    static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
    {
        byte[] encrypted;
        // skapa en ny AesManaged
        using (AesManaged aes = new AesManaged())
        {
            // Skapa encryptor
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            // Skapa MemoryStream
            using (MemoryStream ms = new MemoryStream())
            {
                // Skapar en cryptostream genom att använda cryptoStream klass.
                //Klassen är nyckeln till kryptering som krypterar och dekrypterar data från given stream. I detta fall används memory stream för att kryptera
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    // Skapa Streamwriter och skriv data till stream
                    using (StreamWriter sw = new StreamWriter(cs))
                        sw.Write(plainText);
                    encrypted = ms.ToArray();
                }
            }
        }
        // return krypterad data
        return encrypted;
    }
    static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
    {
        string plaintext = null;
        // Skapa AesManaged
        using (AesManaged aes = new AesManaged())
        {
            // Create a decryptor
            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            // Skapa streams användna för dekryptering
            using (MemoryStream ms = new MemoryStream(cipherText))
            {
                // Skapa crypto stream
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    // Läsa crypto stream
                    using (StreamReader reader = new StreamReader(cs))
                        plaintext = reader.ReadToEnd();
                }
            }
        }
        return plaintext;
    }
}
