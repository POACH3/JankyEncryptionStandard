/// <summary>
/// 
/// Author:       Trenton Stratton
/// Date started: 23-MAR-2024
/// Last updated: 24-MAR-2024
///  
/// Notes:
///     Need to create a menu to select
///     encryption type.
/// 
/// </summary>

using Encrypt;
using System.Numerics;
using System.Text;
using System.Transactions;

string userCont = "";
bool continueRun = true;

//Console.WriteLine("CEASAR CIPHER");
//CeasarCipher ceasar = new CeasarCipher();

Console.WriteLine("RSA");
RSA rsa = new RSA();
//List<string> keys = rsa.GenerateKeysRSA();

//string d = keys[0];
//string n = keys[1];
//string e = keys[2];

//string p = keys[3];
//string q = keys[4];
//string phi = keys[5];

//Console.WriteLine("  private exponent: " + d);
//Console.WriteLine("  modulus: " + n);
//Console.WriteLine("  public exponent: " + e);

//Console.WriteLine("  p: " + p);
//Console.WriteLine("  q: " + q);
//Console.WriteLine("  totient: " + phi);

while (continueRun)
{
    // === RSA STUFF ===

    //Console.WriteLine("\nEnter a message: ");
    //string message = Console.ReadLine();
    //byte[] messageBytes = Encoding.UTF8.GetBytes(message);

    //Console.WriteLine("\n PLAINTEXT CHARS:");
    //foreach (byte b in messageBytes)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //}

    //Console.WriteLine("\n CIPHERTEXT CHARS:");
    //byte[] cipherText = rsa.EncryptRSA(e, n, message);
    //Encoding.UTF8.GetString(cipherText);
    //foreach (byte b in cipherText)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //    //Console.WriteLine("  " + b);
    //}

    //Console.WriteLine("\n DECRYPTED CHARS:");

    ////string plainText = encrypt.DecryptRSA(d, n, cipherText);
    ////Console.WriteLine(plainText);

    //byte[] plainText = rsa.DecryptRSA(d, n, cipherText);
    ////string plainTextUTF8 = Encoding.UTF8.GetString(plainText);
    //foreach (byte b in plainText)
    ////foreach (char c in plainTextUTF8)
    //{
    //    Console.WriteLine("  " + b + " " + Convert.ToChar(b));
    //    //Console.WriteLine("  " + c);
    //}

    Console.WriteLine("\nMODULAR INVERSE:");
    
    try
    {
        Console.WriteLine("Enter n:");
        int num = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter modulo:");
        int mod = int.Parse(Console.ReadLine());

        BigInteger result1 = rsa.ModInverse(num, mod);
        //int result2 = rsa.GCD(num, mod);
        Console.WriteLine("MOD Result: " + result1);
        //Console.WriteLine("GCD Result: " + result2);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    






    // === CEASAR CIPHER STUFF ===

    //Console.WriteLine("\nEnter an encryption value (1-90): ");
    //int offset = int.Parse(Console.ReadLine());

    //Console.WriteLine("Enter a message to encrypt: ");
    //string message = Console.ReadLine();

    //string encryptedMessage = ceasar.Encrypt(offset, message);

    //Console.WriteLine("\nEncrypted: " + encryptedMessage);
    //Console.WriteLine("Decrypted: " + ceasar.Decrypt(offset, encryptedMessage));


    //while (userCont != "Y" || userCont != "N" || userCont != "y" || userCont != "n")
    //{
    //    Console.WriteLine("\nEncrypt another message? (Y/N)");
    //    userCont = Console.ReadLine();
    //    if (userCont == "Y" || userCont == "y")
    //    {
    //        break;
    //    }
    //    else if (userCont == "N" || userCont == "n")
    //    {
    //        run = false;
    //        break;
    //    }
    //    else
    //    {
    //        Console.WriteLine("Invalid command.");
    //    }
    //}

} // end of program while loop